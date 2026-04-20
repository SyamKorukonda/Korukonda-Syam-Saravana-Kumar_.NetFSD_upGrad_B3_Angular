using Microsoft.EntityFrameworkCore;

namespace WebApplication19.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
