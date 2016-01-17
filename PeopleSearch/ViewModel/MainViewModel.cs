using GalaSoft.MvvmLight;
using PeopleSearch.Model;
using System;
using System.Collections.ObjectModel;

namespace PeopleSearch.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

        /// <summary>
        /// Gets Users property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        ObservableCollection<User> _Users;
        public ObservableCollection<User> Users
        {
            get { return _Users; }
            set
            {
                _Users = value;
                RaisePropertyChanged("Users");
            }
        }


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            _dataService.GetUsers(
                (users, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        Console.WriteLine("The following exception has been raised: " + error);
                        return;
                    }

                    Users = users;
                });
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}