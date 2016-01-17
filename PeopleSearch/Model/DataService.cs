﻿using System;
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
    }
}