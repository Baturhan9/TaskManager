using TaskManager.Models.DataTransferObjects;

namespace TaskManager.Interfaces.Services
{
    public interface IProjectService
    {
        IEnumerable<ProjectDTO> GetProjects(bool trackChanges);
        ProjectDTO GetProject(int projectId, bool trackChanges);
        void CreateProject(ProjectDTO project);
        void UpdateProject(int projectId, ProjectDTO project);
        void DeleteProject(int projectId);
    }
}
