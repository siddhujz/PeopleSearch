using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleSearch.Model
{
    public class DataService : IDataService
    {
        ManagerDBContext ctx;
        public DataService()
        {
            ctx = new ManagerDBContext();
        }

        //Method to get users from the database
        public void GetUsers(Action<ObservableCollection<User>, Exception> callback)
        {
            try
            {
                ObservableCollection<User> users = new ObservableCollection<User>();
                foreach (var item in ctx.Users.OrderBy(e => e.FirstName))
                {
                    users.Add(item);
                }
                callback(users, null);
            }
            catch (Exception e)
            {
                callback(new ObservableCollection<User>(), e);
            }
        }

        //Method to get users from the database based on the search criteria(First Name or Last Name)
        public void GetUsersByName(Action<ObservableCollection<User>, Exception> callback, String username)
        {
            try
            {
                var users = ctx.Users.
                    Where(x => x.FirstName.ToLower().Contains(username.ToLower()) || x.LastName.ToLower().Contains(username.ToLower()));

                callback(new ObservableCollection<User> (users), null);
            }
            catch (Exception e)
            {
                callback(new ObservableCollection<User>(), e);
            }
        }

        //Method to add user
        public void CreateUser(Action<int, Exception> callback, User user)
        {
            try
            {
                //Check if a tuple in the table is present in the database, and add it, if it's not available.
                if (ctx.Entry(user).State == System.Data.Entity.EntityState.Detached)
                {
                    ctx.Users.Add(user);
                }
                ctx.SaveChanges();
                callback(user.UserId, null);
            }
            catch (Exception e)
            {
                callback(0, e);
            }
        }
    }
}