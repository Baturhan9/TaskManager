using TaskManager.Models.DataTransferObjects;
using TaskManager.Models.ManipulationDTO;

namespace TaskManager.Interfaces.Services
{
    public interface IProjectService
    {
        IEnumerable<ProjectDTO> GetProjects(int userId, bool trackChanges);
        ProjectDTO GetProject(int projectId, int userId, bool trackChanges);
        ProjectDTO CreateProject(ProjectForManipulationDTO project, int userId);
        void UpdateProject(int projectId, int userId, ProjectForManipulationDTO project);
        void DeleteProject(int projectId, int userId);
    }
}
