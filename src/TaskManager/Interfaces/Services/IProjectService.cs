using TaskManager.Models.DataTransferObjects;
using TaskManager.Models.ManipulationDTO;

namespace TaskManager.Interfaces.Services
{
    public interface IProjectService
    {
        IEnumerable<ProjectDTO> GetProjects(bool trackChanges);
        ProjectDTO GetProject(int projectId, bool trackChanges);
        ProjectDTO CreateProject(ProjectForManipulationDTO project);
        void UpdateProject(int projectId, ProjectForManipulationDTO project);
        void DeleteProject(int projectId);
    }
}
