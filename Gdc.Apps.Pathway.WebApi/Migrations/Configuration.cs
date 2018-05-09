namespace Gdc.Apps.Pathway.WebApi.Migrations
{
    using Gdc.Apps.Pathway.WebApi.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Gdc.Apps.Pathway.WebApi.Models.GdcAppsPathwayWebApiContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Gdc.Apps.Pathway.WebApi.Models.GdcAppsPathwayWebApiContext context)
        {
            context.Leaders.AddOrUpdate(x => x.Id,
          new Leader() { Id = 1, Name = "Leader1" },
        new Leader() { Id = 2, Name = "Leader2" },
        new Leader() { Id = 3, Name = "Leader3" }
        );

            context.Managers.AddOrUpdate(x => x.Id,
new Manager() { Id = 1, Name = "Jane Austen" },
new Manager() { Id = 2, Name = "Charles Dickens" },
new Manager() { Id = 3, Name = "Miguel de Cervantes" }
);
            context.Investments.AddOrUpdate(x => x.Id,
                new Investment() { Id = 1, Name = "HAP", Leader = "Jane", Manager = "Jane Austen", ManagerId = 1 },
                new Investment() { Id = 2, Name = "WP", Leader = "Jane", Manager = "Charles Dickens", ManagerId = 2 },
                new Investment() { Id = 3, Name = "ARS", Leader = "Jane", Manager = "Miguel de Cervantes", ManagerId = 3 }
                );

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
