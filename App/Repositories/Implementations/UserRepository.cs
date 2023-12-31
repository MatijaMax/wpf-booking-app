﻿using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Model.Images;
using BookingProject.Repositories.Intefaces;
using BookingProject.Serializer;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories
{
    public class UserRepository : IUserRepository
    {
        private const string FilePath = "../../Resources/Data/users.csv";
        private Serializer<User> _serializer;
        public List<User> _users;

        public UserRepository()
        {
            _serializer = new Serializer<User>();
            _users = Load();
        }
        public void Initialize() 
        {
            BindUserVouchers();
        }
        public void BindUserVouchers()
        {
            IVoucherRepository voucherRepository = Injector.CreateInstance<IVoucherRepository>();
            foreach (Voucher voucher in voucherRepository.GetAll())
            {
                User user = GetById(voucher.Guest.Id);
                user.Vouchers.Add(voucher);
            }
        }
        public List<User> Load()
        {
            return _serializer.FromCSV(FilePath);
        }
        public void Save()
        {
            _serializer.ToCSV(FilePath, _users);
        }
        private int GenerateId()
        {
            int maxId = 0;
            foreach (User user in _users)
            {
                if (user.Id > maxId)
                {
                    maxId = user.Id;
                }
            }
            return maxId + 1;
        }
        public void Create(User user)
        {
            user.Id = GenerateId();
            _users.Add(user);
            Save();
        }
        public List<User> GetAll()
        {
            return _users.ToList();
        }
        public User GetById(int id)
        {
            return _users.Find(user => user.Id == id);
        }
        public void Update(User user)
        {
            int index = _users.FindIndex(u => u.Id == user.Id);
            _users[index] = user;
            _serializer.ToCSV(FilePath, _users);
        }
    }
}