﻿using BookingProject.View.Guest2ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Navigation;
using BookingProject.Model;
using BookingProject.Controller;
using BookingProject.View.Guest2View;
using BookingProject.View.CustomMessageBoxes;
using BookingProject.Controllers;
using BookingProject.Domain;

namespace BookingProject.View
{
    public partial class SecondGuestHomepageView : Window
    {
        public int GuestId { get; set; }
        public User User { get; set; }
        public UserController UserController { get; set; }
        public TourReservationController TourReservationController { get; set; }    
        public CustomMessageBox CustomMessageBox { get; set; }
        public TourController TourController { get; set; }
        public TourRequestController TourRequestController { get; set; }    
        public SecondGuestHomepageView(int guestId)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.DataContext = this;

            GuestId = guestId;
            User = new User();
            UserController = new UserController();
            TourReservationController = new TourReservationController();
            TourController = new TourController();
            TourRequestController = new TourRequestController();

            FrameHomePage.Content = new SecondGuestProfileView(guestId, this.FrameHomePage.NavigationService);
            CustomMessageBox = new CustomMessageBox();
        }

        private void Button_Click_MyAttendedTours(object sender, RoutedEventArgs e)
        {
            FrameHomePage.Content = new SecondGuestMyAttendedToursView(GuestId, this.FrameHomePage.NavigationService);
        }

        private void Button_Click_MyProfile(object sender, RoutedEventArgs e)
        {
            FrameHomePage.Content = new SecondGuestProfileView(GuestId, this.FrameHomePage.NavigationService);
        }

        private void Button_Click_LogOut(object sender, RoutedEventArgs e)
        {
            TourRequestController.NewlyAcceptedRequests(GuestId);
            UserController.GetLoggedUser().IsLoggedIn = false;
            UserController.Save();
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            this.Close();
        }

        private void Button_Click_SearchAndReservation(object sender, RoutedEventArgs e)
        {
            FrameHomePage.Content = new SerachAndReservationToursView(GuestId, this.FrameHomePage.NavigationService);
        }

        private void Button_Click_MyVouchers(object  sender, RoutedEventArgs e)
        {
            FrameHomePage.Content = new SecondGuestMyVouchersView(GuestId, -1, this.FrameHomePage.NavigationService);
        }

        private void Button_Click_CreateTourRequest(object sender, RoutedEventArgs e)
        {
            FrameHomePage.Content = new CreateTourRequestView(GuestId, this.FrameHomePage.NavigationService);
        }

        private void Button_Click_RequestStatistcis(object sender, RoutedEventArgs e)
        {
            FrameHomePage.Content = new TourRequestStatisticsView(GuestId, this.FrameHomePage.NavigationService);
        }

        private void Button_Click_ActiveTours(object sender, RoutedEventArgs e)
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
                CustomMessageBox.ShowCustomMessageBox("There are currently no active tours that you can follow.");
            }
            else
            {
                FrameHomePage.Content = new ActiveToursView(activeToursIds, GuestId, this.FrameHomePage.NavigationService);                
            }
        }

        private void Button_Click_MyComplexTourRequests(object sender, RoutedEventArgs e)
        {
            FrameHomePage.Content = new ComplexTourRequestsView(GuestId, this.FrameHomePage.NavigationService);
        }

        private void Button_Click_MyReservations(object sender, RoutedEventArgs e)
        {
            FrameHomePage.Content = new SecondGuestMyReservations(GuestId, this.FrameHomePage.NavigationService);
        }

        private void Button_Click_CreateComplexTourRequest(object sender, RoutedEventArgs e)
        {
            FrameHomePage.Content = new CreateComplexTourRequestView(GuestId, this.FrameHomePage.NavigationService);
        }

        private void Button_Click_NewlyCreatedTours(object sender, RoutedEventArgs e)
        {
            if (TourController.FindToursCreatedByStatistcisForGuest(GuestId).Distinct().Count() > 0)
            {
                FrameHomePage.Content = new NewlyCreatedToursOfNeverAcceptedRequestsView(GuestId, this.FrameHomePage.NavigationService);
            }
            else
            {
                CustomMessageBox.ShowCustomMessageBox("There are currently no new tours you have always wanted created according to your requirements.");
            }
        }

        private void Button_Click_MyTourRequests(object sender, RoutedEventArgs e)
        {
            FrameHomePage.Content = new TourRequestsView(GuestId, this.FrameHomePage.NavigationService);
        }
    }
}