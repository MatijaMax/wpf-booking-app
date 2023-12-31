﻿using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Model.Images;
using BookingProject.Repositories.Intefaces;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Implementations
{
    public class AccommodationRepository : IAccommodationRepository
    {
        private const string FilePath = "../../Resources/Data/accommodations.csv";

        private Serializer<Accommodation> _serializer;

        public List<Accommodation> _accommodations;

        public AccommodationRepository()
        {
            _serializer = new Serializer<Accommodation>();
            _accommodations = Load();
        }

        public void Initialize()
        {
            AccommodationLocationBind();
            AccommodationImagesBind();
            AccommodationOwnerBind();
            AccommodationUserBind();
        }

        public List<Accommodation> Load()
        {
            return _serializer.FromCSV(FilePath);
        }
        public void SaveAccommodation()
        {
            _serializer.ToCSV(FilePath, _accommodations);
        }

        public void Save(List<Accommodation> accommodations)
        {
            _serializer.ToCSV(FilePath, accommodations);
        }
        public void Delete(Accommodation accommodationRenovation)
        {
            _accommodations = _serializer.FromCSV(FilePath);
            Accommodation founded = _accommodations.Find(c => c.Id == accommodationRenovation.Id);
            _accommodations.Remove(founded);
            _serializer.ToCSV(FilePath, _accommodations);
        }

        private int GenerateId()
        {
            int maxId = 0;
            foreach (Accommodation accommodation in _accommodations)
            {
                if (accommodation.Id > maxId)
                {
                    maxId = accommodation.Id;
                }
            }
            return maxId + 1;
        }

        public Accommodation GetById(int id)
        {
            return _accommodations.Find(accommodation => accommodation.Id == id);
        }

        public List<Accommodation> GetAll()
        {
            return _accommodations;
        }

        public List<Accommodation> GetAllForOwner(int ownerId)
        {
            List<Accommodation> accommodations = new List<Accommodation>();
            foreach (Accommodation accommodation in _accommodations)
            {
                if (accommodation.Owner.Id == ownerId)
                {
                    accommodations.Add(accommodation);
                }
            }
            return accommodations;
        }

        public void Create(Accommodation accommodation)
        {
            accommodation.Id = GenerateId();
            _accommodations.Add(accommodation);
            Save(_accommodations);
        }

        //public void AddImageToAccommodation(Accommodation accommodation, AccommodationImage image)
        //{
        //    accommodation.Images.Add(image);
        //    image.AccommodationId = accommodation.Id;
        //}


        public void AccommodationLocationBind()
        {
            foreach (Accommodation accommodation in _accommodations)
            {
                Location location = Injector.CreateInstance<IAccommodationLocationRepository>().GetById(accommodation.IdLocation);
                accommodation.Location = location;
            }
        }
        public void AccommodationUserBind()
        {

            foreach (Accommodation accommodation in _accommodations)
            {
                User user = Injector.CreateInstance<IUserRepository>().GetById(accommodation.Owner.Id);
                accommodation.Owner = user;
            }
        }

        public void AccommodationImagesBind()
        {
            IAccommodationImageRepository accommodationImageRepository = Injector.CreateInstance<IAccommodationImageRepository>();
            foreach(AccommodationImage image in accommodationImageRepository.GetAll())
            {
                Accommodation accommodation = GetById(image.AccommodationId);
                accommodation.Images.Add(image);
            }
        }

        public void AccommodationOwnerBind()
        {
            foreach (Accommodation accommodation in _accommodations)
            {
                User owner = Injector.CreateInstance<IUserRepository>().GetById(accommodation.Owner.Id);
                accommodation.Owner = owner;
            }
        }
    }
}
