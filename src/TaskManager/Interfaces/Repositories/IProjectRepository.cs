using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.Interfaces.Repositories
{
    public interface IProjectRepository
    {
        IEnumerable<Project> GetProjects(bool trackChanges);
        Project GetProject(int projectId, bool trackChanges);
        void DeleteProject(Project project);
        void CreateProject(Project project);
    }
}
