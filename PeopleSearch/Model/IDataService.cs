using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace PeopleSearch.Model
{
    public interface IDataService
    {
        void GetUsers(Action<ObservableCollection<User>, Exception> callback);
        void GetUsersByName(Action<ObservableCollection<User>, Exception> callback, String username);
        void CreateUser(Action<int, Exception> callback, User user);
    }
}
