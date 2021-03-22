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

        public async Task<Project> AddNewProject(Project project)
        {
            var result = await _context.AddAsync(project);
            await _context.SaveChangesAsync();
            return result.Entity;
            
        }
        public async Task<IEnumerable<Project>> GetAllProjects()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<Project>GetProjectById(int id)
        {
            return await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Project> UpdateProject(Project project)
        {
            var result = await _context.Projects
                .FirstOrDefaultAsync(e => e.Id == project.Id);

            if(result != null)
            {
                result.Lead = project.Lead;
                result.Name = project.Name;

                await _context.SaveChangesAsync();
                
                return result;
            }

            return null;

        }
    
        public async Task<Project> DeleteProject(int id)
        {
           var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);

           if(project != null)
           {
               _context.Projects.Remove(project);
               await _context.SaveChangesAsync();
               return project;
           }

           return null;
            
        }
    }
}