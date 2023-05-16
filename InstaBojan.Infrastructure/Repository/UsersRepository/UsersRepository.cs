﻿using InstaBojan.Core.Models;
using InstaBojan.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaBojan.Infrastructure.Repository.UsersRepository
{
    public class UsersRepository : IUserRepository
    {
        readonly InstagramStoreContext _context;

        public UsersRepository(InstagramStoreContext context)
        {

            _context = context;
        }

        #region get
        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserById(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            if (user != null)
            {

                return user;
            }
            else
                return null;
        }

        public User GetUserByUserName(string username)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserName == username);
            if (user != null)
            {
                return user;
            }
            else
                return null;
        }

        #endregion

        #region post
        public bool AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }
        #endregion

        #region delete
        public bool DeleteUser(int id)
        {
            var userToDelete = _context.Users.FirstOrDefault(u => u.Id == id);
            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                _context.SaveChanges();
                return true;
            }

            return false;
        }
        #endregion

        #region put
      /*  public bool UpdateUser(User user)
        {
            var userUpd = _context.Users.FirstOrDefault(u => u.Id == user.Id);
            if (userUpd != null)
            {
                userUpd.Id = user.Id;
                userUpd.FirstName = user.FirstName;
                userUpd.LastName = user.LastName;
                userUpd.UserName = user.UserName;
                userUpd.Email = user.Email;
                userUpd.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                _context.Update(userUpd);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        */

        
        public bool UpdateUser(int id, User user) {

            var userUpd = _context.Users.FirstOrDefault(u => u.Id == id);
            if (userUpd != null)
            {
               
                userUpd.FirstName = user.FirstName;
                userUpd.LastName = user.LastName;
                userUpd.UserName = user.UserName;
                userUpd.Email = user.Email;
                userUpd.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                _context.Update(userUpd);
                _context.SaveChanges();
                return true;
            }

            return false;
        }
       

        #endregion

        
    }

}
