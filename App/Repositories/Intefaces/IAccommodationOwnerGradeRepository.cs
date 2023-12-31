﻿using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Intefaces
{
    public interface IAccommodationOwnerGradeRepository
    {
        void Initialize();
        void Create(AccommodationOwnerGrade grade);
        List<AccommodationOwnerGrade> GetAll();
        AccommodationOwnerGrade GetById(int id);
        int GenerateId();
        void Save(List<AccommodationOwnerGrade> grades);
    }
}
