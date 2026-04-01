using Microsoft.EntityFrameworkCore;
namespace WebApplication6.Models
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> products { get; set; }

        public DbSet<Movie> Movies { get; set; }
    }
}
