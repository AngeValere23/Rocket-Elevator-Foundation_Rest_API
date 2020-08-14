using Microsoft.EntityFrameworkCore;

namespace ReStatus.Models
{
    public class elevatorsContext : DbContext
    {
              public elevatorsContext(DbContextOptions<elevatorsContext> options)
            : base(options)
        {
        }

        public DbSet<elevators> elevators { get; set; }        
        public DbSet<batteries> batteries { get; set; }
        public DbSet<columns> columns { get; set; }
        public DbSet<buildings> buildings { get; set; }
        public DbSet<leads> leads { get; set; }
        public DbSet<customers> customers { get; set; }   
        public DbSet<employees> employees { get; set; }  
        public DbSet<quotes> quotes { get; set; }      
        public DbSet<users> users { get; set; }  
        public DbSet<interventions> interventions { get; set; }  
    }
}