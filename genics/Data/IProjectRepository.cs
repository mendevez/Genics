using genics.Dtos.Projects;
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
        Task<Project> CreateProject(Project project);
        Task<Project> UpdateProject(Project project);
        Task DeleteProject(Project project);
    }
}
