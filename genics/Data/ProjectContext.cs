
using genics.Models;
using Microsoft.EntityFrameworkCore;

namespace genics.Data
{
    public class ProjectContext: DbContext
    {

        public ProjectContext(DbContextOptions<ProjectContext> options):base(options)
        {
            
        }

        public DbSet<Project> Projects {get; set;}


    }
}
