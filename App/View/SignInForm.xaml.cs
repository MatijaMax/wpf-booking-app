using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Model.Enums;
using BookingProject.View.GuideView;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BookingProject.Model;
using BookingProject.Domain;
using BookingProject.View.CustomMessageBoxes;
using BookingProject.View.Guest1View;
using BookingProject.ConversionHelp;
using BookingProject.View.OwnerViewModel;
using BookingProject.View.OwnersView;
using System.Globalization;
using BookingProject.Localization;
using System.Threading;

namespace BookingProject.View
{




    /// <summary>
    /// Interaction logic for SignInForm.xaml
    /// </summary>
    public partial class SignInForm : Window
    {
        public static App app;
        public const string ENG = "en-US";
        public const string SRB = "sr-Latn-RS";

        private readonly UserController _controller;
        public bool IsSelectedOwner { get; set; }
        public bool IsSelectedGuest1 { get; set; }
        public bool IsSelectedGuest2 { get; set; }
        public bool IsSelectedGuide { get; set; }
        public static User LoggedInUser { get; set; }
        private TourPresenceController _tourPresenceController { get; set; }
        public NotificationController NotificationController { get; set; }
        private readonly AccommodationReservationController _accResController;
        private readonly RequestAccommodationReservationController _requestController;
        private readonly NotificationController _notificationController;
        private readonly TourRequestController _tourRequestController;
        public CustomNotificationMessageBox CustomNotificationMessageBox { get; set; }
        CustomNotificationMessageBoxNewlyCreatedTours CustomNotificationMessageBoxNewlyCreatedTours { get; set; }
        private SuperGuestController _superGuestController { get; set; }
        //public CustomNotificationMessageBox CustomNotificationMessageBox { get; set; }  

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public SignInForm()
        {
            InitializeComponent();
            DataContext = this;

            _controller = new UserController();
            _accResController = new AccommodationReservationController();
            _tourPresenceController = new TourPresenceController();
            _accResController = new AccommodationReservationController();
            _requestController = new RequestAccommodationReservationController();
            _notificationController = new NotificationController();
            _superGuestController = new SuperGuestController();
            NotificationController = new NotificationController();
            CustomNotificationMessageBox = new CustomNotificationMessageBox();
            CustomNotificationMessageBoxNewlyCreatedTours = new CustomNotificationMessageBoxNewlyCreatedTours();
            _tourRequestController = new TourRequestController();
        }
        private void SignIn(object sender, RoutedEventArgs e)
        {
            User user = _controller.GetByUsername(Username);
            if (user != null)
            {
                if (user.Password == txtPassword.Password)
                {
                    user.numberOfSignIn++;
                    _controller.Update2(user);
                    LoggedInUser = user;
                    if (user.UserType == UserType.OWNER)
                    {
                        Model.User owner = _controller.GetByUsername(Username);
                        owner.IsLoggedIn = true;
                        _controller.Save();
                        //OwnerssView ownerView = new OwnerssView();
                        //ownerView.Show();
                        //List<Notification> notifications = _notificationController.GetOwnerNotifications(owner);
                        //List<Notification> notificationsCopy = new List<Notification>();
                        //foreach (Notification notification in notifications)
                        //{
                        //    MessageBox.Show(notification.Text);
                        //    notificationsCopy.Add(notification);
                        //    _notificationController.DeleteNotificationFromCSV(notification);
                        //}
                        //foreach (Notification notification1 in notificationsCopy)
                        //{
                        //    _notificationController.WriteNotificationAgain(notification1);
                        //}
                        //NotGradedViewModel not_view = new NotGradedViewModel();
                        //int row_num = not_view.RowNum();
                        //if (row_num > 0)
                        //{
                        //    MessageBox.Show("You have " + row_num.ToString() + " guests to rate");
                        //}
                            var view = new MenuView();
                            view.Show();
                        
                    }
                    else if (user.UserType == UserType.GUEST1)
                    {
                        //_controller.GetByUsername(Username).IsLoggedIn = true;
                        Model.User guest = _controller.GetByUsername(Username);
                        guest.IsLoggedIn = true;
                        _controller.Save();
                        _superGuestController.CheckIfGuestIsSuper(guest);
                        Guest1HomepageView guest1View = new Guest1HomepageView();
                        guest1View.Show();
                        List<Notification> notifications = new List<Notification>();
                        notifications = _requestController.GetGuest1Notifications(guest);
                        List<Notification> notificationsCopy = new List<Notification>();
                        foreach (Notification notification in notifications)
                        {
                            MessageBox.Show(notification.Text);
                            notificationsCopy.Add(notification);
                            _notificationController.DeleteNotificationFromCSV(notification);
                        }
                        foreach (Notification notification1 in notificationsCopy)
                        {
                            _notificationController.WriteNotificationAgain(notification1);
                        }
                    }
                    else if (user.UserType == UserType.GUEST2)
                    {
                        _controller.GetByUsername(Username).IsLoggedIn = true;
                        User userGuest = _controller.GetByUsername(Username);
                        _controller.Save();
                        SecondGuestHomepageView homePage = new SecondGuestHomepageView(userGuest.Id);
                        homePage.Show();
                        List<Notification> notifications = new List<Notification>();
                        _tourRequestController.SystemSendingNotification(userGuest.Id);
                        notifications = _tourPresenceController.GetGuestNotifications(userGuest);
                        List<Notification> notificationsCopy = new List<Notification>();

                        foreach (Notification notification in notifications)
                        {
                            if (notification.RelatedTo.Equals("Creating a tour on demand"))
                            {
                                CustomNotificationMessageBoxNewlyCreatedTours.ShowCustomNotification(notification.Text, userGuest);
                            }
                            else if (notification.RelatedTo.Equals("System notification about new tours"))
                            {
                                CustomNotificationMessageBoxNewlyCreatedTours.ShowCustomNotification(notification.Text, userGuest);
                                //ovo cu izmeniti kad se klikne ok da se otvore te nove kreirane ture
                            }
                            else
                            {
                                CustomNotificationMessageBox.ShowCustomMessageBoxNotification(notification.Text, userGuest);
                            }
                            notificationsCopy.Add(notification);
                            _tourPresenceController.DeleteNotificationFromCSV(notification);
                        }

                        foreach (Notification notification1 in notificationsCopy)
                        {
                            _tourPresenceController.WriteNotificationAgain(notification1);
                        }
                    }
                    else if (user.UserType == UserType.GUIDE)
                    {


                        _controller.GetByUsername(Username).IsLoggedIn = true;
                        _controller.Save();
                        GuideHomeWindow guideHomeWindow = new GuideHomeWindow();
                        guideHomeWindow.Show();
                    }
                    if (user.UserType == UserType.RESIGNED)
                    {
                        MessageBox.Show("This user resigned!");
                    }
                    else
                    {
                        Close();
                    }

                }
                else
                {
                    MessageBox.Show("Wrong password!");
                }
            }
            else
            {
                MessageBox.Show("Wrong username!");
            }
        }
    }
}