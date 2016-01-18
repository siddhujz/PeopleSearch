namespace PeopleSearch.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PeopleSearch.Model.ManagerDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            //AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(PeopleSearch.Model.ManagerDBContext context)
        {
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

            context.Users.AddOrUpdate(
                u => u.FirstName,
                new Model.User { UserId = 1, FirstName = "Siddhartha", LastName = "Kodali", DateOfBirth = new DateTime(1992, 01, 09), AddressLine1 = "1100 Oakcrest St", AddressLine2 = "Apt J", City = "Iowa City", State = "Iowa", Zipcode = "52246", Interests = "I like playing Play Fifa, Cricket, Badminton" },
                new Model.User { UserId = 2, FirstName = "Balaji", LastName = "Thiruppathi", DateOfBirth = new DateTime(1992, 04, 19), AddressLine1 = "1100 Oakcrest St", AddressLine2 = "Apt J", City = "Iowa City", State = "Iowa", Zipcode = "52246", Interests = "I love coding, building android applications." }
                );
        }
    }
}
