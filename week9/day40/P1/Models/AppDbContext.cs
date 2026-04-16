using Microsoft.EntityFrameworkCore;

namespace ContactManagement.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ContactInfo> Contacts { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One Company -> Many Contacts
            modelBuilder.Entity<ContactInfo>()
                .HasOne(c => c.Company)
                .WithMany(comp => comp.Contacts)
                .HasForeignKey(c => c.CompanyId);

            // One Department -> Many Contacts
            modelBuilder.Entity<ContactInfo>()
                .HasOne(c => c.Department)
                .WithMany(dept => dept.Contacts)
                .HasForeignKey(c => c.DepartmentId);
        }
    }
}
