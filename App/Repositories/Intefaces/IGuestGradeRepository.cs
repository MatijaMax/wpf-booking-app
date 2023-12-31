﻿using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Intefaces
{
    public interface IGuestGradeRepository
    {
        void Create(GuestGrade guestGrade);
        List<GuestGrade> GetAll();
        GuestGrade GetById(int id);
        void Initialize();
        void Save(List<GuestGrade> grades);
    }
}
