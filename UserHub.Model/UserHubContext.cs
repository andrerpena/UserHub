using System;
using System.Data.Entity;
using System.Net.NetworkInformation;
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

        public DbSet<Suggestion> Suggestions { get; set; }

        public DbSet<Tenancy> Tenancies { get; set; }

        public DbSet<Vote> Votes { get; set; }

        public DbSet<SuggestionCategory> SuggestionCategories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Vote configuration
            modelBuilder.Entity<Vote>().HasKey(m => m.Id);
            modelBuilder.Entity<Vote>().HasRequired(m => m.CreatedBy).WithMany(m => m.Votes).WillCascadeOnDelete(false);
            modelBuilder.Entity<Vote>().HasOptional(m => m.Suggestion).WithMany(m => m.Votes);
            modelBuilder.Entity<Vote>().HasRequired(m => m.Tenancy).WithMany(m => m.Votes);

            // Suggestion configuration
            modelBuilder.Entity<Suggestion>().HasKey(m => m.Id);
            modelBuilder.Entity<Suggestion>().HasRequired(m => m.CreatedBy).WithMany(m => m.Suggestions).WillCascadeOnDelete(false);
            modelBuilder.Entity<Suggestion>().Property(m => m.Title).IsRequired();
            modelBuilder.Entity<Suggestion>().Property(m => m.Description).HasColumnType("varchar(max)").IsRequired();
            modelBuilder.Entity<Suggestion>().HasRequired(m => m.Tenancy).WithMany(m => m.Suggestions);

            // Suggestion category configuration
            modelBuilder.Entity<SuggestionCategory>().HasKey(m => m.Id);
            modelBuilder.Entity<SuggestionCategory>().Property(m => m.DisplayName).IsRequired();
            modelBuilder.Entity<SuggestionCategory>().HasRequired(m => m.Tenancy).WithMany(m => m.SuggestionCategories);

            // Tenancy configuration
            modelBuilder.Entity<Tenancy>().HasKey(m => m.Id);
            modelBuilder.Entity<Tenancy>().HasRequired(m => m.CreatedBy).WithMany(m => m.Tenancies).WillCascadeOnDelete(false);
        }
    }
}