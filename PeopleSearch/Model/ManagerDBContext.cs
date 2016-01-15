using PeopleSearch.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleSearch.Model
{
    class ManagerDBContext : DbContext
    {
        public ManagerDBContext() : base("name=PeopleSearchDBConnectionString") 
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ManagerDBContext>());
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ManagerDBContext>());
        }

        public DbSet<User> Users { get; set; }
    }
}
