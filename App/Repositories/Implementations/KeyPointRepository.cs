﻿using BookingProject.Model;
using BookingProject.Model.Enums;
using BookingProject.Repositories.Intefaces;
using BookingProject.Serializer;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Implementations
{
    public class KeyPointRepository : IKeyPointRepository
    {
        private const string FilePath = "../../Resources/Data/keyPoints.csv";
        private Serializer<KeyPoint> _serializer;
        public List<KeyPoint> _keyPoints;

        public KeyPointRepository() {
            _serializer = new Serializer<KeyPoint>();
            _keyPoints = Load();
        }
        public void Initialize() { }
        public List<KeyPoint> Load()
        {
            return _serializer.FromCSV(FilePath);
        }
        public void Save()
        {
            _serializer.ToCSV(FilePath, _keyPoints);
        }
        private int GenerateId()
        {
            int maxId = 0;
            foreach (KeyPoint keyPoint in _keyPoints)
            {
                if (keyPoint.Id > maxId)
                {
                    maxId = keyPoint.Id;
                }
            }
            return maxId + 1;
        }
        public void Create(KeyPoint keyPoint)
        {
            keyPoint.Id = GenerateId();
            _keyPoints.Add(keyPoint);
            Save();
        }
        public List<KeyPoint> GetAll()
        {
            return _keyPoints;
        }
        public KeyPoint GetById(int id)
        {
            return _keyPoints.Find(keyPoint => keyPoint.Id == id);
        }
    }
}