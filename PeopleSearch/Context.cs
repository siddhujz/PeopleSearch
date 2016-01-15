using PeopleSearch.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleSearch
{
    class Context: DbContext
    {
        public Context() : base("name=PeopleSearchDBConnectionString") 
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
