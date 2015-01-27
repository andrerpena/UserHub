using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using UserHub.Model.Entities;
using UserHub.Model.Helpers;

namespace UserHub.Model.Seed
{
    public static class SeedHelper
    {
        public static void Seed(UserHubContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

            // deletes all data
            DeleteAllData(context);

            // creates users
            SeedUsers(context);

            // creates tenancies
            SeedTenancies(context);

            // creates suggestions
            SeedSuggestions(context);
        }

        /// <summary>
        /// Deletes all data
        /// </summary>
        /// <param name="context"></param>
        private static void DeleteAllData(DbContext context)
        {
            if (context == null) throw new ArgumentNullException("context");
            context.Database.ExecuteSqlCommand("sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'");
            context.Database.ExecuteSqlCommand("sp_MSForEachTable 'IF OBJECT_ID(''?'') NOT IN (ISNULL(OBJECT_ID(''[dbo].[__MigrationHistory]''),0)) DELETE FROM ?'");
            context.Database.ExecuteSqlCommand("EXEC sp_MSForEachTable 'ALTER TABLE ? CHECK CONSTRAINT ALL'");
        }

        private static void SeedTenancies(UserHubContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

            var adminUser = context.Users.First();

            var tenancy = new Tenancy()
            {
                CreatedOn = DateTimeOffset.Now,
                CreatedBy = adminUser,
                Name = "userhub"
            };

            context.Tenancies.Add(tenancy);

            context.SaveChanges();
        }

        /// <summary>
        /// Creates the users
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static void SeedUsers(DbContext context)
        {
            if (context == null) throw new ArgumentNullException("context");
            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);

            manager.Create(new ApplicationUser { Email = "andrerpena@gmail.com", UserName = "andrerpena@gmail.com" }, "123123");

            context.SaveChanges();
        }

        private static void SeedSuggestions(UserHubContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

            var random = new Random();

            var suggestionNames = new List<string>()
            {
                "Wouldn't it be nice if Visual Studio could be x64 and work like PortableApps from a thumbdrive?",
                "XAML Debugging",
                "Bring back Classic Visual Basic, an improved version of Visual Basic 6.0 (the old idea has been stoped at 7400 votes for no good reason)",
                "Make WPF open-source and accept pull-requests from the community",
                "Improve WPF performance",
                "OOTB Rust programming langague support",
                "Store project related information in .vs folder to avoid polluting the root of the project",
                "Provide Lightswitch Product Roadmap - recurring Town Hall"
            };

            foreach (var suggestionName in suggestionNames)
            {
                var createdBy = RandomHelper.GetRandomObject<ApplicationUser>(context, random);
                var createdOn = RandomHelper.GetRandomDate(DateTimeOffset.Now.AddDays(-30), 20, random);
                var description = RandomHelper.GetRandomText(3, 15, 1, 3, 1, 3, random);
                var tenancy = context.Tenancies.First();

                context.Suggestions.Add(
                    new Suggestion()
                    {
                        Title = suggestionName,
                        CreatedBy = createdBy,
                        CreatedOn = createdOn,
                        Description = description,
                        Tenancy = tenancy
                    });
            }

            context.SaveChanges();
        }
    }
}
