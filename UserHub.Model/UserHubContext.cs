using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using UserHub.Model.Entities;

namespace UserHub.Model
{
    // You can add profile data for the user by adding more properties to your User class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class UserHubContext : IdentityDbContext<ApplicationUser>
    {
        public UserHubContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Vote configuration
            modelBuilder.Entity<Vote>().HasKey(m => m.Id);
            modelBuilder.Entity<Vote>().HasRequired(m => m.CreatedBy).WithMany(m => m.Votes);
            modelBuilder.Entity<Vote>().HasOptional(m => m.Suggestion).WithMany(m => m.Votes);

            // Suggestion configuration
            modelBuilder.Entity<Suggestion>().HasKey(m => m.Id);
            modelBuilder.Entity<Suggestion>().HasRequired(m => m.CreatedBy).WithMany(m => m.Suggestions);
            modelBuilder.Entity<Suggestion>().Property(m => m.Title).IsRequired();
            modelBuilder.Entity<Suggestion>().Property(m => m.Description).HasColumnType("varchar(max)").IsRequired();

            // Tenancy configuration
            modelBuilder.Entity<Tenancy>().HasKey(m => m.Id);
            modelBuilder.Entity<Tenancy>().HasRequired(m => m.CreatedBy).WithMany(m => m.Tenancies);
        }
    }
}