using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace WebApplication8.Models
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Dept> Depts { get; set; }

       


        protected override void OnModelCreating(ModelBuilder modelBuilder) // here we combine both because it allowed 
                                                                           // only one method 
        {
            // Employee -> Dept
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Dept)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DeptId);

            
        }


    }
}
