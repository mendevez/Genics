using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using genics.Models;
using Microsoft.EntityFrameworkCore;

namespace genics.Data
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RequestResponse <Project>> AddNewProject(Project project)
        {

            var result = await _context.AddAsync(project);
            await _context.SaveChangesAsync();
            return new RequestResponse<Project>() { Data = result.Entity, Message = "Project created successfully", Success = true };
            
        }
        public async Task<RequestResponse<IEnumerable<Project>>> GetAllProjects()
        {
            RequestResponse<IEnumerable<Project>> response = new RequestResponse<IEnumerable<Project>>();

            response.Data = await _context.Projects.ToListAsync();
            response.Message = "Project list acquired successfully";
            response.Success = true;

            return response;
            
        }

        public async Task<RequestResponse<Project>>GetProjectById(int id)
        {
            RequestResponse<Project> response = new RequestResponse<Project>();
            response.Data = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
            response.Message = "Request successful";
            response.Success = true;

            return response;
        }

        public async Task<RequestResponse<Project>> UpdateProject(Project project)
        {
            var result = await _context.Projects
                .FirstOrDefaultAsync(e => e.Id == project.Id);

            if(result != null)
            {
                result.Lead = project.Lead;
                result.Name = project.Name;

                await _context.SaveChangesAsync();
                
                return new RequestResponse<Project> { Data = result, Message = "Project updated successfully", Success = true };
            }

            return null;

        }
    
        public async Task<RequestResponse<Project>> DeleteProject(int id)
        {
           var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);

           if(project != null)
           {
               var result = _context.Projects.Remove(project);
               await _context.SaveChangesAsync();

               return new RequestResponse<Project> { Data = result.Entity, Message = "Successfully deleted project", Success = true } ;
           }

           return new RequestResponse<Project> { Message = "Project not found", Success = false, Data = null };

        }
    }
}