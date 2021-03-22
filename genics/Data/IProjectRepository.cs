using genics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace genics.Data
{
    public interface IProjectRepository
    {
        Task<RequestResponse<IEnumerable<Project>>> GetAllProjects();
        Task<RequestResponse<Project>> GetProjectById(int id);
        Task<RequestResponse<Project>> AddNewProject(Project project);
        Task<RequestResponse<Project>> UpdateProject(Project project);
        Task<RequestResponse<Project>> DeleteProject(int id);
    }
}
