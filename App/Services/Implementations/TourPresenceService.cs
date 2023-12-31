﻿using BookingProject.Controller;
using BookingProject.Controllers;
using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Repositories.Intefaces;
using BookingProject.Repositories;
using BookingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingProject.Repositories.Implementations;
using BookingProject.Serializer;

namespace BookingProject.Services.Implementations
{
    public class TourPresenceService : ITourPresenceService
    {
        private ITourPresenceRepository _tourPresenceRepository;
        private ITourTimeInstanceRepository _tourTimeInstanceRepository;
        private ITourService _tourService;
        private INotificationService _notificationService;
        private INotificationRepository _notificationRepository;

        public TourPresenceService() { }
        public void Initialize()
        {
            _tourPresenceRepository = Injector.CreateInstance<ITourPresenceRepository>();
            _tourTimeInstanceRepository = Injector.CreateInstance<ITourTimeInstanceRepository>();
            _tourService = Injector.CreateInstance<ITourService>();
            _notificationService = Injector.CreateInstance<INotificationService>();
            _notificationRepository = Injector.CreateInstance<INotificationRepository>();
        }
        public void Create(TourPresence presence)
        {
           _tourPresenceRepository.Create(presence);
        }
        public List<TourPresence> GetAll()
        {
            return _tourPresenceRepository.GetAll();
        }
        public TourPresence GetById(int id)
        {
            return _tourPresenceRepository.GetById(id);
        }
        public void SendNotification(User guest)
        {
            Notification notification = new Notification();
            notification.UserId = guest.Id;
            notification.Text = "Presence check !!! ";
            notification.Read = false;
            _notificationService.Create(notification);
        }
        public List<Notification> GetGuestNotifications(User guest)
        {
            List<Notification> notificationsForGuest = new List<Notification>();
            List<Notification> _notifications = _notificationService.GetAll();

            foreach (Notification notification in _notifications)
            {
                if (notification.UserId == guest.Id && notification.Read == false)
                {
                    notificationsForGuest.Add(notification);
                }
            }

            return notificationsForGuest;
        }
        public List<Tour> FindAttendedTours(User guest)
        {
            List<int> tourInstanceIds = new List<int>();
            List<TourReservation> attendedTours = new List<TourReservation>();
            List<TourTimeInstance> tourTimeInstances = _tourTimeInstanceRepository.GetAll(); ;
            List<Tour> tours = new List<Tour>();
            List<Tour> potentialTours = new List<Tour>();

            foreach (TourPresence tp in _tourPresenceRepository.GetAll())
            {
                if (tp.UserId == guest.Id && tp.KeyPointId != -1)
                {
                    tourInstanceIds.Add(tp.TourId);
                }
            }

            foreach (TourTimeInstance tti in tourTimeInstances)
            {
                foreach (int id in tourInstanceIds)
                {
                    if (id == tti.Id)
                    {
                        Tour tour = new Tour();
                        tour = _tourService.GetById(tti.TourId);
                        tours.Add(tour);
                    }
                }
            }
            return tours.Distinct().ToList();
        }
        public void DeleteNotificationFromCSV(Notification notification)
        {
            _notificationService.DeleteNotificationFromCSV(notification);
        }
        public void WriteNotificationAgain(Notification n)
        {
            Notification notification = new Notification();
            notification.UserId = n.UserId;
            notification.Text = n.Text;
            notification.Read = true;
            _notificationService.Create(notification);
        }
        public void Save()
        {
            _tourPresenceRepository.Save();
        }
    }
}