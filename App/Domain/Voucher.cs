﻿using BookingProject.Model.Enums;
using BookingProject.Model;
using BookingProject.Serializer;
using ISerializable = BookingProject.Serializer.ISerializable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingProject.Domain.Enums;
using BookingProject.ConversionHelp;
using System.Runtime.Serialization;

namespace BookingProject.Domain
{
    public class Voucher : ISerializable
    {
        public int Id { get; set; }
        public User Guest { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public VoucherState State { get; set; }
        public Tour Tour { get; set; }
        public bool IsAward { get; set; }

        public Voucher()
        {
            State = VoucherState.CREATED;
            Guest = new User();
            Tour = new Tour();
            IsAward = false;
        }

        public Voucher(int id, User guest, DateTime startDate, DateTime endDate, VoucherState state, Tour tour, bool isAward)
        {
            Id = id;
            Guest = guest;
            StartDate = startDate;
            EndDate = endDate;
            State = state;
            Tour = tour;
            IsAward = isAward;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Guest.Id = int.Parse(values[1]);
            StartDate = DateConversion.StringToDateTour(values[2]);
            EndDate = DateConversion.StringToDateTour(values[3]);
            VoucherState voucherState;
            if (Enum.TryParse<VoucherState>(values[4], out voucherState))
            {
                State = voucherState;
            }
            else
            {
                voucherState = VoucherState.CREATED;
                System.Console.WriteLine("An error occurred while loading the state!");
            }
            Tour.Id = int.Parse(values[5]);
            IsAward = bool.Parse(values[6]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Guest.Id.ToString(),
                StartDate.ToString(),
                EndDate.ToString(),
                State.ToString(),
                Tour.Id.ToString(),
                IsAward.ToString()
            };
            return csvValues;
        }
    }
}
