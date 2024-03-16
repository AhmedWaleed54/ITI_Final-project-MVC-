using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Data
{
    public class ApplicationDbContext : DbContext
    {
       public DbSet<Rating> Rating { get; set; }
       public DbSet<Movie> Movie { get; set; }

        public DbSet<Actor> Actor { get; set; }
        
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
