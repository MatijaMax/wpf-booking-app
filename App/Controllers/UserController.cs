﻿using BookingProject.DependencyInjection;
using BookingProject.Domain;
using BookingProject.Model;
using BookingProject.Serializer;
using BookingProject.Services.Interfaces;
using OisisiProjekat.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Controller
{
    public class UserController 
    {
        private readonly IUserService _userService;
        private readonly ISuperGuideService _superGuideService;
        public UserController()
        {
            _userService = Injector.CreateInstance<IUserService>();
            _superGuideService = Injector.CreateInstance<ISuperGuideService>();
        }
        public User GetByUsername(string username)
        {
            return _userService.GetByUsername(username);
        }
        public User GetLoggedUser()
        {
            return _userService.GetLoggedUser();
        }
        public void Create(User user)
        {
            _userService.Create(user);
        }
        public List<User> GetAll()
        {
            return _userService.GetAll();   
        }
        public User GetById(int id)
        {
            return _userService.GetById(id);
        }
        public void Save()
        {
            _userService.Save();
        }
        public void Update2(User user)
        {
            _userService.Update2(user);
        }

        public void GoSuper()
        {
            _superGuideService.SetToSuper();
        }

        //public bool isUserSuperUser(User user)
        //{
        //    return _userService.IsUserSuperUser(user);
        //}
    }
}