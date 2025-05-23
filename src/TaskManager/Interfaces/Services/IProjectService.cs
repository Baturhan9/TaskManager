using TaskManager.Models.CreateModelObjects;
using TaskManager.Models.DataTransferObjects;

namespace TaskManager.Interfaces.Services
{
    public interface IProjectService
    {
        IEnumerable<ProjectDTO> GetProjects(bool trackChanges);
        ProjectDTO GetProject(int projectId, bool trackChanges);
        void CreateProject(ProjectCreateDTO project);
        void UpdateProject(int projectId, ProjectCreateDTO project);
        void DeleteProject(int projectId);
    }
}
