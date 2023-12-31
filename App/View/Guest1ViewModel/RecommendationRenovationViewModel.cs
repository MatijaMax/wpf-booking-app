﻿using BookingProject.Commands;
using BookingProject.Controllers;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.View.Guest1View;
using BookingProject.View.Guest1View.Tutorials;
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
    public class RecommendationRenovationViewModel : INotifyPropertyChanged 
    {
        public AccommodationReservation AccommodationReservation { get; set; }
        public RecommendationRenovationController recommendationRenovationController { get; set; }

        public bool SelectedFirst { get; set; }
        public bool SelectedSecond { get; set; }
        public bool SelectedThird { get; set; }
        public bool SelectedFourth { get; set; }
        public bool SelectedFifth { get; set; }

        public RecommendationRenovation RecommendationRenovation;
        public RelayCommand HomePageCommand { get; }
        public RelayCommand MyReservationsCommand { get; }
        public RelayCommand LogOutCommand { get; }
        public RelayCommand MyReviewsCommand { get; }
        public RelayCommand SendCommand { get; }
        public RelayCommand MyProfileCommand { get; }
        public RelayCommand CreateForumCommand { get; }
        public RelayCommand QuickSearchCommand { get; }
        public RelayCommand TutorialCommand { get; }
        public RecommendationRenovationViewModel(AccommodationReservation selectedReservation)
        {
            AccommodationReservation = selectedReservation;
            recommendationRenovationController = new RecommendationRenovationController();
            RecommendationRenovation = new RecommendationRenovation();
            HomePageCommand = new RelayCommand(Button_Click_Homepage, CanExecute);
            MyReservationsCommand = new RelayCommand(Button_Click_MyReservations, CanExecute);
            LogOutCommand = new RelayCommand(Button_Click_Logout, CanExecute);
            MyReviewsCommand = new RelayCommand(Button_Click_MyReviews, CanExecute);
            SendCommand = new RelayCommand(Button_Click_Send, CanExecute);
            MyProfileCommand = new RelayCommand(Button_Click_MyProfile, CanExecute);
            CreateForumCommand = new RelayCommand(Button_Click_CreateForum, CanExecute);
            QuickSearchCommand = new RelayCommand(Button_Click_Quick_Search, CanExecute);
            TutorialCommand = new RelayCommand(Button_Click_Tutorial, CanExecute);
        }


        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }

        public void SetLevel()
        {
            if (SelectedFirst)
            {
                RecommendationRenovation.UrgencyLevel = 1;
            }else if (SelectedSecond)
            {
                RecommendationRenovation.UrgencyLevel = 2;
            }else if (SelectedThird)
            {
                RecommendationRenovation.UrgencyLevel = 3;
            }else if (SelectedFourth)
            {
                RecommendationRenovation.UrgencyLevel = 4;
            }
            else
            {
                RecommendationRenovation.UrgencyLevel = 5;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool CanExecute(object param) { return true; }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(RecommendationRenovationView)) { window.Close(); }
            }
        }

        private void Button_Click_MyReservations(object param)
        {
            var res = new Guest1Reservations();
            res.Show();
            CloseWindow();
        }

        private void Button_Click_Homepage(object param)
        {
            var ghp = new Guest1HomepageView();
            ghp.Show();
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

        private void Button_Click_Send(object param)
        {
            RecommendationRenovation.Description = Description;
            RecommendationRenovation.AccommodationReservation.Id = AccommodationReservation.Id;
            SetLevel();
            recommendationRenovationController.Create(RecommendationRenovation);
            MessageBox.Show("You have successfully send recommendation for renovation!");
            var reservations = new Guest1Reservations();
            reservations.Show();
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

        private void Button_Click_Tutorial(object param)
        {
            var tutorial = new AccommodationOwnerReviewTutorialView();
            tutorial.Show();
            CloseWindow();
        }
    }
}
