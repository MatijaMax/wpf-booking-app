﻿using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Intefaces
{
    public interface ITourTimeInstanceRepository
    {
        void Create(TourTimeInstance instance);
        List<TourTimeInstance> GetAll();
        TourTimeInstance GetById(int id);
        void InstanceDateBind();
        void InstanceTourBind();
        void Initialize();
        void Save();

        void BindLastTour();
    }
}
