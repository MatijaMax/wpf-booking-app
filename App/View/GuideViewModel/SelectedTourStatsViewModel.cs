﻿using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.Domain.Enums;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.View.GuideView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BookingProject.Commands;

namespace BookingProject.View.GuideViewModel
{
    public class SelectedTourStatsViewModel
    {
        public string TeenGuests { get; set; } = "LOREM IPSUM";
        public string AdultGuests { get; set; } = "LOREM IPSUM";
        public string OldGuests { get; set; } = "LOREM IPSUM";
        public string VoucherGuests { get; set; } = "LOREM IPSUM";
        public string VoucherlessGuests { get; set; } = "LOREM IPSUM";
        public List<int> GuestNumbers { get; set; }
        public double vouchersPercentage { get; set; } = 0;
        public TourTimeInstance ChosenTour { get; set; }
        private UserController _userControler;
        private VoucherController _voucherController;
        private TourReservationController _tourReservationController;
        public RelayCommand OkCommand { get; }
        public SelectedTourStatsViewModel(TourTimeInstance chosenTour)
        {
            ChosenTour = chosenTour;
            _userControler = new UserController();
            _tourReservationController = new TourReservationController();
            _voucherController = new VoucherController();
            GuestNumbers = GetGuestNumbers();
            vouchersPercentage = GetVoucherPercentage();
            SetValues();
            OkCommand = new RelayCommand(Button_Click, CanExecute);

        }
        public void SetValues()
        {
            TeenGuests = GuestNumbers[0].ToString();
            AdultGuests = GuestNumbers[1].ToString();
            OldGuests = GuestNumbers[2].ToString();
            VoucherGuests = Math.Round(vouchersPercentage, 2).ToString() + "%";
            VoucherlessGuests = Math.Round((100 - vouchersPercentage), 2).ToString() + "%";
        }
        public double GetVoucherPercentage()
        {
            double voucherCount = 0;
            double guestCount = GuestNumbers[0] + GuestNumbers[1] + GuestNumbers[2];
            foreach (Voucher voucher in _voucherController.GetAll())
            {
                if (voucher.Tour.Id == ChosenTour.TourId && voucher.State == VoucherState.USED)
                {
                    voucherCount += 1;
                }
            }
            return voucherCount / guestCount * 100;
        }
        public List<int> GetGuestNumbers()
        {
            List<int> guestNumbers = new List<int>();
            int Teen = 0; guestNumbers.Add(Teen);
            int Adult = 0; guestNumbers.Add(Adult);
            int Senior = 0; guestNumbers.Add(Senior);

            foreach (TourReservation reservation in FilterTourReservations(_tourReservationController.GetAll()))
            {
                if (_userControler.GetById(reservation.Guest.Id).Age < 18)
                {
                    guestNumbers[0] += 1;
                }
                else if (_userControler.GetById(reservation.Guest.Id).Age > 50)
                {
                    guestNumbers[2] += 1;
                }
                else
                {
                    guestNumbers[1] += 1;
                }
            }
            return guestNumbers;
        }
        public List<TourReservation> FilterTourReservations(List<TourReservation> reservations)
        {
            List<TourReservation> filteredReservations = new List<TourReservation>();
            foreach (TourReservation reservation in reservations)
            {
                if (reservation.Tour.Id == ChosenTour.TourId)
                {
                    filteredReservations.Add(reservation);
                }
            }
            return filteredReservations;
        }

        private bool CanExecute(object param) { return true; }
        private void CloseWindow()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window.GetType() == typeof(SelectedTourStatsWindow)) { window.Close(); }
            }
        }

        private void Button_Click(object param)
        {
            TourStatisticsWindow tourStatisticsWindow = new TourStatisticsWindow("all");
            tourStatisticsWindow.Show();
            CloseWindow();
        }
    }
}

