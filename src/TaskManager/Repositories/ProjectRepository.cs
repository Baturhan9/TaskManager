using TaskManager.Interfaces.Repositories;
using TaskManager.Models;

namespace TaskManager.Repositories
{
    public class ProjectRepository : RepositoryBase<Project>, IProjectRepository
    {
        public ProjectRepository(TaskManagerContext context)
            : base(context) { }

        public IEnumerable<Project> GetProjects(bool trackChanges) =>
            FindAll(trackChanges).OrderBy(p => p.ShortName).ToList();

        public Project GetProject(int projectId, bool trackChanges) =>
            FindByCondition(p => p.ProjectId.Equals(projectId), trackChanges).SingleOrDefault();

        public void DeleteProject(Project project) => Delete(project);

        public void CreateProject(Project project) => Create(project);
    }
}
