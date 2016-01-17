using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using PeopleSearch.MessageInfrastructure;
using PeopleSearch.Model;
using PeopleSearch.Services;
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
        /// The declaration used in case of searching people
        /// </summary>
        string _username;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                RaisePropertyChanged("Username");
            }
        }

        #region Command Object Declarations
        public RelayCommand ReadAllUsersCommand { get; set; }
        public RelayCommand SearchUsersCommand { get; set; }
        public RelayCommand SendUserCommand { get; set; }
        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;

            Users = new ObservableCollection<User>();
            ReadAllUsersCommand = new RelayCommand(GetUsers);

            //Execute ReadAllUsersCommand to display all users on the main window when the application launches
            if (ReadAllUsersCommand.CanExecute(null))
                ReadAllUsersCommand.Execute(null);

            SearchUsersCommand = new RelayCommand(SearchUsers);

            SendUserCommand = new RelayCommand(SendEstimate);
        }


        /// <summary>
        /// Method to Read All Users
        /// </summary>
        /// 
        public void GetUsers()
        {
            Users.Clear();
            Username = "";
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

        /// <summary>
        /// The method to search users based upon their name(firstname or lastname)
        /// </summary>
        public void SearchUsers()
        {
            Users.Clear();
            if(Username != null && Username.Trim().Length > 0)
            {
                _dataService.GetUsersByName(
                    (users, error) =>
                    {
                        if (error != null)
                        {
                        // Report error here
                        Console.WriteLine("The following exception has been raised: " + error);
                            return;
                        }
                        Users = users;
                    }, Username);
            } else
            {
                GetUsers();
            }
        }

        /// <summary>
        /// The method to send a message to add a new user
        /// to the UserViewModel
        /// </summary>
        public void SendEstimate()
        {
            User user = new User();

            //Send User as a message to UserViewModel for opening a new window.
            Messenger.Default.Send<MessageCommunicator>(new MessageCommunicator() { User = user }, MessengerToken.AddUser);
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}