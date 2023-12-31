﻿using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Domain
{
    public class Notification : ISerializable
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public bool Read { get; set; }
        public string RelatedTo { get; set; }

        public Notification () { }
        public Notification(int id, int userId, string text, bool read, string relatedTo)
        {
            Id = id;
            UserId = userId;
            Text = text;
            Read = read;
            RelatedTo = relatedTo;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            UserId = int.Parse(values[1]);
            Text = values[2];
            Read = bool.Parse(values[3]);
            RelatedTo = values[4];
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                UserId.ToString(),
                Text,
                Read.ToString(),
                RelatedTo
            };
            return csvValues;
        }
    }
}
