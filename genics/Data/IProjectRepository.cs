using genics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace genics.Data
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllProjects();
        Task<Project> GetProjectById(int id);
        Task<Project> AddNewProject(Project project);
        Task<Project> UpdateProject(Project project);
        Task<Project> DeleteProject(int id);
    }
}
