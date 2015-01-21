using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using UserHub.Model.Entities;

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
    }
}
