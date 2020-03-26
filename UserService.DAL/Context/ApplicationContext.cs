using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UserService.DAL.Models;

namespace UserService.DAL.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserProfile> UserProfiles { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
