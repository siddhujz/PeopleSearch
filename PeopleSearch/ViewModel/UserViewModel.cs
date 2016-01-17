using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using PeopleSearch.MessageInfrastructure;
using PeopleSearch.Model;
using PeopleSearch.Services;
using System;

namespace PeopleSearch.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class UserViewModel : ViewModelBase
    {
        /// <summary>
        /// The declaration used in case of adding new user
        /// </summary>
        User _user;

        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                RaisePropertyChanged("User");
            }
        }

        /// <summary>
        /// Initializes a new instance of the UserViewModel class.
        /// </summary>
        public UserViewModel()
        {
            Receive();
        }

        /// <summary>
        /// The Method used to Receive the send User from the Main Window
        /// and assigning it the the UserNotifiable property so that
        /// it will be displayed on the view
        /// </summary>
        void Receive()
        {
            Messenger.Default.Register<MessageCommunicator>(this, MessengerToken.AddUser, (user) => ShowAddUser(user));
        }

        private void ShowAddUser(MessageCommunicator user)
        {
            User = user.User;

            WindowService windowService = new WindowService();
            windowService.showWindow(this);
        }
    }
}