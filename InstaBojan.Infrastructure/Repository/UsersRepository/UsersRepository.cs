using InstaBojan.Core.Models;
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
        public bool AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
             return true;
        }

        public bool DeleteUser(int id)
        {
            var userToDelete=_context.Users.FirstOrDefault(u => u.Id == id);
            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                return true;
            }

            return false;
        }

        public User GetUserById(int id)
        {
            var user= _context.Users.FirstOrDefault(x => x.Id == id);
            if (user == null) {
                throw new ApplicationException("User Not Found ");
            }
              return user;
        }

        public User GetUserByUserName(string username)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserName == username);
           /* if (user == null)
            {
                throw new Exception("UserName doesn't exist");
            }

            */

            return user;
                
                
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public bool UpdateUser(User user)
        {
            var userUpd = _context.Users.FirstOrDefault(x => x.Id == user.Id);
            if (userUpd != null)
            {
                _context.Entry(user).State = EntityState.Modified;
                return true;
            }

            return false;
        }
    }
}
