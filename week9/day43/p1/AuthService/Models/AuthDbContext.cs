using Microsoft.EntityFrameworkCore;

namespace AuthService.Models
{
    public class AuthDbContext:DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }
        public DbSet<UserModel> Users { get; set; }
    }
}
