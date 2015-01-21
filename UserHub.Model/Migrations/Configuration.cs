using UserHub.Model.Seed;

namespace UserHub.Model.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<UserHub.Model.UserHubContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(UserHub.Model.UserHubContext context)
        {
            SeedHelper.Seed(context);
        }
    }
}
