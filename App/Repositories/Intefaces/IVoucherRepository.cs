﻿using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Intefaces
{
    public interface IVoucherRepository
    {
        void Create(Voucher voucher);
        List<Voucher> GetAll();
        Voucher GetById(int id);
        void Initialize();
        void Save(List<Voucher> vouchers);
        void SaveVouchers();
        void CreatePrizeVoucher(User guest);
    }
}