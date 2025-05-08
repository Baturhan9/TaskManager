using AutoMapper;
using TaskManager.Exceptions.ModelsExceptions.NotFoundExceptions;
using TaskManager.Interfaces.Repositories;
using TaskManager.Interfaces.Services;
using TaskManager.Models;
using TaskManager.Models.DataTransferObjects;

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

        public void CreateProject(ProjectDTO project)
        {
            var projectDB = _mapper.Map<Project>(project);
            _repositoryManager.Project.CreateProject(projectDB);
            _repositoryManager.Save();
        }

        public void DeleteProject(int projectId)
        {
            var project = _repositoryManager.Project.GetProject(projectId, trackChanges: false);
            if (project == null)
                throw new NotFoundProjectException(projectId);

            _repositoryManager.Project.DeleteProject(project);
            _repositoryManager.Save();
        }

        public ProjectDTO GetProject(int projectId, bool trackChanges)
        {
            var project = _repositoryManager.Project.GetProject(projectId, trackChanges);
            if (project == null)
                throw new NotFoundProjectException(projectId);

            return _mapper.Map<ProjectDTO>(project);
        }

        public IEnumerable<ProjectDTO> GetProjects(bool trackChanges)
        {
            var projects = _repositoryManager.Project.GetProjects(trackChanges);
            return _mapper.Map<IEnumerable<ProjectDTO>>(projects);
        }

        public void UpdateProject(int projectId, ProjectDTO project)
        {
            var projectDB = _repositoryManager.Project.GetProject(projectId, trackChanges: true);
            if (projectDB == null)
                throw new NotFoundProjectException(projectId);

            _mapper.Map(project, projectDB);
            _repositoryManager.Save();
        }
    }
}
