using AutoMapper;
using TaskManager.Exceptions.ForbiddenExceptions;
using TaskManager.Exceptions.ModelsExceptions.NotFoundExceptions;
using TaskManager.Interfaces.Repositories;
using TaskManager.Interfaces.Services;
using TaskManager.Models;
using TaskManager.Models.DataTransferObjects;
using TaskManager.Models.ManipulationDTO;

namespace TaskManager.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ProjectService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public ProjectDTO CreateProject(ProjectForManipulationDTO project, int userId)
        {
            var user = _repositoryManager.User.GetUser(userId, trackChanges: false);
            if (user is null)
                throw new NotFoundUserException(userId);

            if (project.DateOfCreate.HasValue)
            {
                project.DateOfCreate = DateTime.SpecifyKind(
                    project.DateOfCreate.Value,
                    DateTimeKind.Utc
                );
            }

            var projectDB = _mapper.Map<Project>(project);
            _repositoryManager.Project.CreateProject(projectDB);
            _repositoryManager.Save();

            var createdProject = _mapper.Map<ProjectDTO>(projectDB);

            var userAccess = new UserAccess()
            {
                UserId = userId,
                ProjectId = createdProject.ProjectId,
            };

            _repositoryManager.UserAccess.CreateUserAccess(userAccess);
            _repositoryManager.Save();

            return createdProject;
        }

        public void DeleteProject(int projectId, int userId)
        {
            var project = TryGetProject(projectId, userId, trackChanges: false);
            _repositoryManager.Project.DeleteProject(project);
            _repositoryManager.Save();
        }

        public ProjectDTO GetProject(int projectId, int userId, bool trackChanges)
        {
            var project = TryGetProject(projectId, userId, trackChanges);
            return _mapper.Map<ProjectDTO>(project);
        }

        public IEnumerable<ProjectDTO> GetProjects(int userId, bool trackChanges)
        {
            var user = _repositoryManager.User.GetUser(userId, trackChanges: false);
            if (user is null)
                throw new NotFoundUserException(userId);

            var userAccessesProjectId = _repositoryManager
                .UserAccess.GetUserAccessesByUserId(userId, trackChanges: false)
                .Select(u => u.ProjectId)
                .ToList();

            var projects = _repositoryManager
                .Project.GetProjects(trackChanges)
                .Where(p => userAccessesProjectId.Contains(p.ProjectId));
            return _mapper.Map<IEnumerable<ProjectDTO>>(projects);
        }

        public void UpdateProject(int projectId, int userId, ProjectForManipulationDTO project)
        {
            var projectDB = TryGetProject(projectId, userId, trackChanges: true);
            _mapper.Map(project, projectDB);
            _repositoryManager.Save();
        }

        private Project TryGetProject(int projectId, int userId, bool trackChanges)
        {
            var projectDB = _repositoryManager.Project.GetProject(projectId, trackChanges);
            if (projectDB is null)
                throw new NotFoundProjectException(projectId);

            var user = _repositoryManager.User.GetUser(userId, trackChanges: false);
            if (user is null)
                throw new NotFoundUserException();

            var userAccess = _repositoryManager.UserAccess.GetUserAccessesByUserIdAndProjectId(
                userId,
                projectId,
                trackChanges: false
            );
            if (userAccess is null)
                throw new ProjectForbiddenException(projectId);

            return projectDB;
        }
    }
}
