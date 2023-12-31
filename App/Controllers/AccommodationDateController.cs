﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingProject.DependencyInjection;
using BookingProject.Model;
using BookingProject.Services.Interfaces;
using OisisiProjekat.Observer;

namespace BookingProject.Controller
{
    public class AccommodationDateController 
    {
        private readonly IAccommodationDateService _accommodationDateService;
        public AccommodationDateController()
        {
            _accommodationDateService = Injector.CreateInstance<IAccommodationDateService>();
        }
        public List<AccommodationDate> GetAll()
        {
            return _accommodationDateService.GetAll();
        }
        public AccommodationDate GetById(int id)
        {
            return _accommodationDateService.GetById(id);
        }
        public void Save(List<AccommodationDate> dates)
        {
            _accommodationDateService.Save(dates);
        }
        public bool CheckEnteredDates(DateTime initialDate, DateTime endDate)
        {
            return _accommodationDateService.CheckEnteredDates(initialDate, endDate);
        }
        public bool CompareWithToday(DateTime initialDate)
        {
            return _accommodationDateService.CompareWithToday(initialDate);
        }
        public bool CheckDays(DateTime initialDate, DateTime endDate)
        {
            return _accommodationDateService.CheckDays(initialDate, endDate);
        }
        public bool CheckAvailableDate(Accommodation selectedAccommodation, DateTime initialDate, DateTime endDate, int numberOfDaysToStay, string numberOfGuests)
        {
            return _accommodationDateService.CheckAvailableDate(selectedAccommodation, initialDate, endDate, numberOfDaysToStay, numberOfGuests);
        }
        public List<AccommodationReservation> GetReservationsForAccommodation(Accommodation accommodation)
        {
            return _accommodationDateService.GetReservationsForAccommodation(accommodation);
        }
        public List<DateTime> MakeListOfReservedDates(DateTime initialDate, DateTime endDate)
        {
            return _accommodationDateService.MakeListOfReservedDates(initialDate, endDate);
        }
        public bool IsDateReserved(List<DateTime> tryDates, List<AccommodationReservation> reservations)
        {
            return _accommodationDateService.IsDateReserved(tryDates, reservations);
        }
        public List<(DateTime, DateTime)> FindAvailableDates(Accommodation selectedAccommodation, DateTime initialDate, DateTime endDate, int numberOfDaysToStay)
        {
            return _accommodationDateService.FindAvailableDates(selectedAccommodation, initialDate, endDate, numberOfDaysToStay);
        }
        public bool IfDatesAreInTakenList(List<DateTime> datesToCheck, List<DateTime> takenDates)
        {
            return _accommodationDateService.IfDatesAreInTakenList(datesToCheck, takenDates);
        }
        public List<DateTime> FindTakenDates(Accommodation selectedAccommodation)
        {
            return _accommodationDateService.FindTakenDates(selectedAccommodation);
        }
        public List<(DateTime, DateTime)> FindAvailableDatesQuick(Accommodation accommodation, int daysToStay)
		{
            return _accommodationDateService.FindAvailableDatesQuick(accommodation, daysToStay);
		}
        public List<(DateTime, DateTime)> FindAvailableDatesQuickRanges(Accommodation accommodation, int daysToStay, DateTime initialDate, DateTime endDate)
		{
            return _accommodationDateService.FindAvailableDatesQuickRanges(accommodation, daysToStay, initialDate, endDate);
		}
        public bool IsDateAvailable(DateTime date, int daysToStay, List<AccommodationReservation> reservations)
		{
            return _accommodationDateService.IsDateAvailable(date, daysToStay, reservations);
		}
    }
}