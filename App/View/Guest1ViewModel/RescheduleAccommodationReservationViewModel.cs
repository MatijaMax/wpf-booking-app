﻿using BookingProject.Commands;
using BookingProject.Controllers;
using BookingProject.Model;
using BookingProject.View.Guest1View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingProject.View.Guest1ViewModel
{
    public class RescheduleAccommodationReservationViewModel
    {
        public RequestAccommodationReservationController RequestAccommodationReservationController { get; set; }
        public AccommodationReservation SelectedReservation;
        public RelayCommand SendRequestCommand { get; }
        public RelayCommand SeeRequestsCommand { get; }
        public RelayCommand HomePageCommand { get; }
        public RelayCommand MyReservationsCommand { get; }
        public RelayCommand LogOutCommand { get; }
        public RelayCommand MyReviewsCommand { get; }
        public RelayCommand MyProfileCommand { get; }
        public RelayCommand CreateForumCommand { get; }
        public RelayCommand QuickSearchCommand { get; }
        public RescheduleAccommodationReservationViewModel(AccommodationReservation selectedReservation)
        {
            RequestAccommodationReservationController = new RequestAccommodationReservationController();
            SelectedReservation = new AccommodationReservation();
            SelectedReservation = selectedReservation;
            NewInitialDate = DateTime.Now;
            NewEndDate = DateTime.Now;
            SendRequestCommand = new RelayCommand(Button_Click_Send_Request, CanExecute);
            SeeRequestsCommand = new RelayCommand(Button_Click_See_Requests, CanExecute);
            HomePageCommand = new RelayCommand(Button_Click_Homepage, CanExecute);
            MyReservationsCommand = new RelayCommand(Button_Click_MyReservations, CanExecute);
            LogOutCommand = new RelayCommand(Button_Click_Logout, CanExecute);
            MyReviewsCommand = new RelayCommand(Button_Click_MyReviews, CanExecute);
            MyProfileCommand = new RelayCommand(Button_Click_MyProfile, CanExecute);
            CreateForumCommand = new RelayCommand(Button_Click_CreateForum, CanExecute);
            QuickSearchCommand = new RelayCommand(Button_Click_Quick_Search, CanExecute);
        }
        private bool CanExecute(object param) { return true; }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(RescheduleAccommodationReservationView)) { window.Close(); }
            }
        }

        public DateTime _newInitialDate;
        public DateTime NewInitialDate
        {
            get => _newInitialDate;
            set
            {
                if (_newInitialDate != value)
                {
                    _newInitialDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime _newEndDate;
        public DateTime NewEndDate
        {
            get => _newEndDate;
            set
            {
                if (_newEndDate != value)
                {
                    _newEndDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public String _comment;
        public String Comment
        {
            get => _comment;
            set
            {
                if (_comment != value)
                {
                    _comment = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click_Send_Request(object param)
        {
            if(NewInitialDate == null || NewEndDate == null)
            {
                MessageBox.Show("You must enter new initial and new end date!");
            }else if (NewInitialDate > NewEndDate)
            {
                MessageBox.Show("New initial date must be before new end date!");
            }else if(NewInitialDate == DateTime.Now.AddHours(0).AddMinutes(0).AddSeconds(0))
            {
                MessageBox.Show("New initial date must be after today!");
            }
            else
            {
                RequestAccommodationReservationController.SendRequest(SelectedReservation, Comment, NewInitialDate, NewEndDate);
                MessageBox.Show("Successfully rescheduled reservation!");
                var reservations = new Guest1Reservations();
                reservations.Show();
                CloseWindow();
            }
        }
        private void Button_Click_See_Requests(object param)
        {
            var reqAccView = new AccommodationRequestsView();
            reqAccView.Show();
            CloseWindow();
        }

        private void Button_Click_Homepage(object param)
        {
            var Guest1Homepage = new Guest1HomepageView();
            Guest1Homepage.Show();
            CloseWindow();
        }

        private void Button_Click_MyReservations(object param)
        {
            var Guest1Reservations = new Guest1Reservations();
            Guest1Reservations.Show();
            CloseWindow();
        }

        private void Button_Click_Logout(object param)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();
            CloseWindow();
        }

        private void Button_Click_MyReviews(object param)
        {
            var reviews = new Guest1ReviewsView();
            reviews.Show();
            CloseWindow();
        }
        private void Button_Click_MyProfile(object param)
        {
            var profile = new Guest1ProfileView();
            profile.Show();
            CloseWindow();
        }
        private void Button_Click_CreateForum(object param)
        {
            var forum = new OpenForumView();
            forum.Show();
            CloseWindow();
        }

        private void Button_Click_Quick_Search(object param)
        {
            var quickS = new QuickSearchView();
            quickS.Show();
            CloseWindow();
        }
    }
}
