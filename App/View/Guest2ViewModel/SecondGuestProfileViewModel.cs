﻿using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.DependencyInjection;
using BookingProject.Model;
using BookingProject.Repositories.Intefaces;
using BookingProject.View.Guest2View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace BookingProject.View.Guest2ViewModel
{
    public class SecondGuestProfileViewModel : INotifyPropertyChanged
    {
        private int GuestId { get; set; }
        public UserController UserController { get; set; }
        public User User { get; set; }
        public Tour Tour { get; set; }
        public TourReservationController TourReservationController { get; set; }
        public ITourReservationRepository _tourReservationRepository;
        public RelayCommand MyAttendedToursCommand { get; }
        public RelayCommand MyReservationsCommand { get; }
        public RelayCommand SerachAndReservationToursCommand { get; }
        public RelayCommand MyVouchersCommand { get; }
        public RelayCommand LogOutCommand { get; }
        public RelayCommand MonitoringActiveToursCommand { get; }
        public RelayCommand CreateTourRequestCommand { get; }
        public RelayCommand MyTourRequestsCommand { get; }
        public RelayCommand RequestStatisticsCommand { get; }
        public RelayCommand ProfileCommand { get; }
        public TourReservation TourReservation { get; set; }
        public string UsernameDisplay { get; set; }
        public NavigationService NavigationService { get; set; }
        public string NameDisplay { get; set; }
        public string SurnameDisplay { get; set; }
        public string EmailDiplay { get; set; }
        public string GenderDisplay { get; set; }

        public SecondGuestProfileViewModel(int guestId, NavigationService navigationService)
        {
            this.GuestId = guestId;

            NavigationService = navigationService;

            UserController = new UserController();
            User = new User();
            Tour = new Tour();
            TourReservation = new TourReservation();
            TourReservationController = new TourReservationController();
            _tourReservationRepository = Injector.CreateInstance<ITourReservationRepository>();

            MyAttendedToursCommand = new RelayCommand(Button_Click_MyAttendedTours, CanExecute);
            MyReservationsCommand = new RelayCommand(Button_Click_MyReservations, CanExecute);
            SerachAndReservationToursCommand = new RelayCommand(Button_Click_SerachAndReservationTours, CanExecute);
            MyVouchersCommand = new RelayCommand(Button_Click_MyVouchers, CanExecute);
            LogOutCommand = new RelayCommand(Button_Click_LogOut, CanExecute);
            MonitoringActiveToursCommand = new RelayCommand(Button_Click_MonitoringActiveTours, CanExecute);
            CreateTourRequestCommand = new RelayCommand(Button_Click_CreateTourRequest, CanExecute);
            MyTourRequestsCommand = new RelayCommand(Button_Click_MyTourRequests, CanExecute);
            RequestStatisticsCommand = new RelayCommand(Button_Click_RequestStatistics, CanExecute);
            ProfileCommand = new RelayCommand(Button_Click_Profile, CanExecute);

            if (GuestId == 5)
            {
                PictureSource = new Uri("https://www.shutterstock.com/image-photo/selfie-portrait-happy-senior-woman-260nw-1851945448.jpg");
                NameDisplay = "Anica";
                SurnameDisplay = "Anic";
                EmailDiplay = "anicaanic65@gmail.com";
                GenderDisplay = "female";
            }
            else if (GuestId == 3)
            {
                PictureSource = new Uri("https://media.istockphoto.com/id/996843044/photo/who-said-selfies-are-just-for-the-youth.jpg?s=612x612&w=0&k=20&c=NWD4bqyxkopGtNycCWAgV9XJXiJ4VTMgCwz0jQg1Eb8=");
                NameDisplay = "Mile";
                SurnameDisplay = "Milic";
                EmailDiplay = "milemilic65@gmail.com";
                GenderDisplay = "male";
            }
            UsernameDisplay = UserController.GetById(GuestId).Username;

        }

        private Uri pictureSource;

        public Uri PictureSource
        {
            get { return pictureSource; }
            set
            {
                if (pictureSource != value)
                {
                    pictureSource = value;
                    OnPropertyChanged(nameof(PictureSource));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(SecondGuestProfileView)) { window.Close(); }
            }
        }

        private bool CanExecute(object param) { return true; }

        private void Button_Click_Profile(object param)
        {
            //SecondGuestProfileView profile = new SecondGuestProfileView(GuestId);
            //profile.Show();
            CloseWindow();
        }

        private void Button_Click_MyAttendedTours(object param)
        {
            //SecondGuestMyAttendedToursView secondGuestMyAttendedTours = new SecondGuestMyAttendedToursView(GuestId);
            //secondGuestMyAttendedTours.Show();
            CloseWindow();
        }
        private void Button_Click_MyReservations(object param)
        {
            //SecondGuestMyReservations secondGuestMyReservations = new SecondGuestMyReservations(GuestId);
            //secondGuestMyReservations.Show();
            CloseWindow();
        }

        private void Button_Click_SerachAndReservationTours(object param)
        {
            //SerachAndReservationToursView serachAndReservationTours = new SerachAndReservationToursView(GuestId);
            //serachAndReservationTours.Show();
            CloseWindow();
        }

        private void Button_Click_MyVouchers(object param)
        {
            //SecondGuestMyVouchersView secondGuestMyVouchers = new SecondGuestMyVouchersView(GuestId, -1);
            //secondGuestMyVouchers.Show();
            CloseWindow();
        }

        private void Button_Click_LogOut(object param)
        {
            UserController.GetById(GuestId);
            User.Id = GuestId;
            User.IsLoggedIn = false;
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            CloseWindow();
        }
        private void Button_Click_MonitoringActiveTours(object param)
        {
            List<TourReservation> tourReservations = new List<TourReservation>();
            tourReservations = TourReservationController.GetAll();
            int flag = 0;
            List<TourReservation> activeTours = new List<TourReservation>();
            List<int> activeToursIds = new List<int>();

            foreach (TourReservation tr in tourReservations)
            {
                if (GuestId == tr.Guest.Id && tr.ReservationStartingTime.Date == DateTime.Now.Date)
                {
                    flag = 1;
                    activeTours.Add(tr);
                    activeToursIds.Add(tr.Tour.Id);
                }
            }
            if (flag != 1)
            {
                MessageBox.Show("There are currently no active tours that you can follow.");
            }
            else
            {
                //ActiveToursView activeToursView = new ActiveToursView(activeToursIds, GuestId);
                //activeToursView.Show();
                CloseWindow();
            }
        }
        private void Button_Click_CreateTourRequest(object param)
        {
            //CreateTourRequestView createTourRequestView = new CreateTourRequestView(GuestId);
            //createTourRequestView.Show();
            //CloseWindow();
        }
        private void Button_Click_MyTourRequests(object param)
        {
            //TourRequestsView tourRequestsView = new TourRequestsView(GuestId);
            //tourRequestsView.Show();
            CloseWindow();
        }
        private void Button_Click_RequestStatistics(object param)
        {
            //TourRequestStatisticsView tourRequestStatisticsView = new TourRequestStatisticsView(GuestId);
            //tourRequestStatisticsView.Show();
            CloseWindow();
        }
    }
}