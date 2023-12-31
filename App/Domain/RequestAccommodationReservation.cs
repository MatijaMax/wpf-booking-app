﻿using BookingProject.ConversionHelp;
using BookingProject.Domain.Enums;
using BookingProject.Model;
using BookingProject.Serializer;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Domain
{
    public class RequestAccommodationReservation : ISerializable
    {
        public int Id { get; set; }
        public AccommodationReservation AccommodationReservation { get; set; }
        public DateTime NewArrivalDay { get; set; }
        public DateTime NewDeparuteDay { get; set; }
        public RequestStatus Status { get; set; }
        public String GuestComment { get; set; }
        public String OwnerComment { get; set; }


        public RequestAccommodationReservation(AccommodationReservation accommodationReservation, DateTime newArrivalDay, DateTime newDeparuteDay, RequestStatus status, string comment, string comment2)
        {
            AccommodationReservation = accommodationReservation;
            NewArrivalDay = newArrivalDay;
            NewDeparuteDay = newDeparuteDay;
            Status = status;
            GuestComment = comment;
            OwnerComment = comment2;
        }

        public RequestAccommodationReservation()
        {
            AccommodationReservation = new AccommodationReservation();
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            AccommodationReservation.Id = int.Parse(values[1]);
            NewArrivalDay = DateConversion.StringToDateAccommodation(values[2]);
            NewDeparuteDay = DateConversion.StringToDateAccommodation(values[3]);
            RequestStatus status;

            if (Enum.TryParse<RequestStatus>(values[4], out status))
            {
                Status = status;
            }
            else
            {
                Status = RequestStatus.PENDING;
                System.Console.WriteLine("Doslo je do greske prilikom ucitavanja tipa zahteva!");
            }
            GuestComment = values[5];
            OwnerComment = values[6];
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                AccommodationReservation.Id.ToString(),
                DateConversion.DateToStringAccommodation(NewArrivalDay),
                DateConversion.DateToStringAccommodation(NewDeparuteDay),
                Status.ToString(),
                GuestComment,
                OwnerComment
            };
            return csvValues;
        }
    }
}
