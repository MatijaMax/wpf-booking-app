﻿using BookingProject.Model;
using BookingProject.View.OwnersViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingProject.View.OwnersView
{
    /// <summary>
    /// Interaction logic for ChoseAccommodationForRenovationView.xaml
    /// </summary>
    public partial class ChoseAccommodationForRenovationView : Page
    {
        public ChoseAccommodationForRenovationView(NavigationService navigationService)
        {
            InitializeComponent();
            this.DataContext = new ChoseAccommodationForRenovationViewModel(navigationService);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var accommodation = ((Button)sender).DataContext as Accommodation;
            NavigationService.Navigate(new EnterAccommodationRenovationDatesView(accommodation, NavigationService));
        }
    }
}
