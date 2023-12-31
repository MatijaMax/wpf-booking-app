﻿using BookingProject.Controller;
using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Domain.Enums;
using BookingProject.Model;
using BookingProject.Model.Images;
using BookingProject.Serializer;
using BookingProject.Services.Interfaces;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Controllers
{
    public class VoucherController
    {
        private readonly IVoucherService _voucherService;
        public VoucherController()
        {
            _voucherService = Injector.CreateInstance<IVoucherService>();
        }
        public void DeleteExpiredVouchers()
        {
            _voucherService.DeleteExpiredVouchers();
        }
        public List<Voucher> GetUserVouhers(int guestId)
        {
            return _voucherService.GetUserVouhers(guestId);
        }
        public void Create(Voucher voucher)
        {
            _voucherService.Create(voucher);
        }
        public List<Voucher> GetAll()
        {
            return _voucherService.GetAll();    
        }
        public Voucher GetById(int id)
        {
            return _voucherService.GetById(id);
        }
        public void Save(List<Voucher> _vouchers)
        {
            _voucherService.Save(_vouchers);
        }
        public void SaveVouchers()
        {
            _voucherService.SaveVouchers();
        }
        public void CreatePrizeVoucher(User guest)
        {
            _voucherService.CreatePrizeVoucher(guest);
        }
        public List<Voucher> GetAllGuestsVouchers(int guestId)
        {
            return _voucherService.GetAllGuestsVouchers(guestId);
        }
    }
}