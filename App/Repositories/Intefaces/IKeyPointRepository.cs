﻿using BookingProject.Model;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Intefaces
{
    public interface IKeyPointRepository
    {
       void Create(KeyPoint keyPoint);
       List<KeyPoint> GetAll();
       KeyPoint GetById(int id);
       void Initialize();
       void Save();
    }
}
