﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BookingProject.Model;
using BookingProject.View;
using OisisiProjekat.Observer;
using BookingProject.ConversionHelp;
using BookingProject.Controllers;
using BookingProject.Domain;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using BookingProject.Repositories.Intefaces;
using BookingProject.DependencyInjection;
using BookingProject.Services.Interfaces;
using BookingProject.Repositories.Implementations;
using System.Windows.Navigation;
using BookingProject.Services;

namespace BookingProject.Controller
{
    public class TourReservationController
    {
        private ITourReservationService _tourReservationService;
        public TourReservationController()
        {
            _tourReservationService = Injector.CreateInstance<ITourReservationService>();
        }
        public bool BookingSuccess(Tour chosenTour, string numberOfGuests, DateTime selectedDate, User guest, NavigationService navigationService)
        {
            return _tourReservationService.BookingSuccess(chosenTour, numberOfGuests, selectedDate, guest, navigationService);
        }
        public bool GoThroughReservations(Tour chosenTour, string numberOfGuests, DateTime selectedDate, User guest, NavigationService navigationService)
        {
            return _tourReservationService.GoThroughReservations(chosenTour, numberOfGuests, selectedDate, guest, navigationService);
        }
        public bool TryReservation(Tour chosenTour, string numberOfGuests, DateTime selectedDate, User guest)
        {
            return _tourReservationService.TryReservation(chosenTour, numberOfGuests, selectedDate, guest);
        }
        public void TryToBook(Tour chosenTour, string numberOfGuests, DateTime selectedDate, User guest, NavigationService navigationService)
        {
            _tourReservationService.TryToBook(chosenTour, numberOfGuests, selectedDate, guest, navigationService);
        }
        public void FullyBookedTours(Tour chosenTour, DateTime selectedDate, User guest, NavigationService navigationService)
        {
            _tourReservationService.FullyBookedTours(chosenTour, selectedDate, guest, navigationService);
        }

        public void SuccessfulReservationMessage(string numberOfGuests, User guest, Tour chosenTour, NavigationService navigationService) 

        {
            _tourReservationService.SuccessfulReservationMessage(numberOfGuests, guest, chosenTour, navigationService);
        }
        public void FreePlaceMessage(int maxGuests)
        {
            _tourReservationService.FreePlaceMessage(maxGuests);
        }
        public List<TourReservation> GetAll()
        {
            return _tourReservationService.GetAll();
        }
        public TourReservation GetById(int id)
        {
            return _tourReservationService.GetById(id);
        }
        public List<TourReservation> GetUserReservations(int guestId)
        {
            return _tourReservationService.GetUserReservations(guestId);
        }
    }
}