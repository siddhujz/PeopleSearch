using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using PeopleSearch.MessageInfrastructure;
using PeopleSearch.Model;
using PeopleSearch.Services;
using System;
using System.Linq;
using System.Windows;
using System.Drawing;
using System.IO;

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
        private readonly IDataService _dataService;

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

        #region Command Object Declarations
        public RelayCommand<User> SaveUserCommand { get; set; }
        #endregion

        /// <summary>
        /// Initializes a new instance of the UserViewModel class.
        /// </summary>
        public UserViewModel(IDataService dataService)
        {
            _dataService = dataService;
            ReceiveAddUserMessage();

            SaveUserCommand = new RelayCommand<User>(SaveUser);
        }

        /// <summary>
        /// The Method used to Receive the send User from the Main Window
        /// and assigning it the the UserNotifiable property so that
        /// it will be displayed on the view
        /// </summary>
        void ReceiveAddUserMessage()
        {
            Messenger.Default.Register<MessageCommunicator>(this, MessengerToken.AddUser, (user) => ShowAddUser(user));
        }

        private void ShowAddUser(MessageCommunicator user)
        {
            User = user.User;

            WindowService windowService = new WindowService();
            windowService.showWindow(this);
        }

        /// <summary>
        /// Method to Save User. Once the User is added in the database
        /// it will be added in the Users observable collection and Property Changed will be raised
        /// </summary>
        /// <param name="user"></param>
        void SaveUser(User user)
        {
            Color bgcolor = Color.Black;
            Color fcolor = Color.WhiteSmoke;
            Image image = ConvertTextToImage(user.FirstName.Substring(0, 1) + user.LastName.Substring(0,1), "Times New Roman", 25, bgcolor, fcolor, 50, 50);
            user.UserPhoto = ImageToByteArray(image);
            _dataService.CreateUser(
                (UserId, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        Console.WriteLine("The following exception has been raised: " + error);
                        return;
                    }
                    if (UserId != 0)
                    {
                        //Close the current focused window
                        var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                        window.Close();

                        //Send a message to Main ViewModel for refreshing the People on the MainWindow.
                        Messenger.Default.Send(new NotificationMessage("RefreshUsers"), MessengerToken.RefreshUsers);
                    }
                }, user);
        }

        public Bitmap ConvertTextToImage(string txt, string fontname, int fontsize, Color bgcolor, Color fcolor, int width, int Height)
        {
            Bitmap bmp = new Bitmap(width, Height);
            using (Graphics graphics = Graphics.FromImage(bmp))
            { 
                Font font = new Font(fontname, fontsize);
                Brush brush = Brushes.Black;
                graphics.FillRectangle(brush, 0, 0, bmp.Width, bmp.Height);
                graphics.DrawString(txt, font, new SolidBrush(fcolor), 0, 0);
                graphics.Flush();
                font.Dispose();
                graphics.Dispose();
            }
            return bmp;
        }

        public byte[] ImageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

    }
}