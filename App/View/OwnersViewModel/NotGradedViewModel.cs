﻿using BookingProject.Commands;
using BookingProject.Controller;
using BookingProject.Model;
using BookingProject.View.CustomMessageBoxes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace BookingProject.View.OwnerViewModel
{
    public class NotGradedViewModel
    {
        private AccommodationReservationController _accommodationController;
        public ObservableCollection<AccommodationReservation> Reservations { get; set; }

        public AccommodationReservation SelectedReservation { get; set; }
        public RelayCommand GradeCommand { get; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand MenuCommand { get; }
        public NavigationService NavigationService { get; set; }
        public OwnerNotificationCustomBox box { get; set; }
        public NotGradedViewModel(NavigationService navigationService)
        {
            

            //var app = Application.Current as App;
            //_accommodationController = app.AccommodationReservationController;
            _accommodationController = new AccommodationReservationController();


            Reservations = new ObservableCollection<AccommodationReservation>(_accommodationController.GetAllNotGradedReservations(SignInForm.LoggedInUser.Id));
            GradeCommand = new RelayCommand(Button_Grade, CanExecute);
            CancelCommand = new RelayCommand(Button_Cancel, CanExecute);
            MenuCommand = new RelayCommand(Button_Click_Menu, CanExecute);
            box = new OwnerNotificationCustomBox(); 
            NavigationService = navigationService;
        }
        //private void selectedIndexChanged(object sender, EventArgs e)
        //{
        //    var selectedItem = SelectedReservation;
        //    var window2 = new GuestRateViewModel(selectedItem);
        //    window2.SelectedObject = selectedItem;
        //    window2.Show();
        //}
        private void Button_Click_Menu(object param)
        {
            MenuView view = new MenuView();
            view.Show();
            CloseWindow();
        }
        private bool CanExecute(object param) { return true; }
        private void Button_Grade(object param)
        {
            if (SelectedReservation != null)
            {
                //GuestRateView view = new GuestRateView(SelectedReservation);
                //view.Show();
                //CloseWindow();
                NavigationService.Navigate(new GuestRateView(SelectedReservation, NavigationService));
            } else
            {
                box.ShowCustomMessageBox("You need to select a guest you want to grade!");
            }
        }
        private void Button_Cancel(object param)
        {
            NavigationService.Navigate(new OwnerssView(NavigationService));
            //var view = new OwnerssView();
            //view.Show();
            //CloseWindow();
        }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(NotGradedView)) { window.Close(); }
            }
        }
        public int RowNum()
        {
            return Reservations.Count;
        }
    }
}
