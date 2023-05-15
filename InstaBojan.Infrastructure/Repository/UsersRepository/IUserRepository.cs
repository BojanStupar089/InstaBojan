using InstaBojan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaBojan.Infrastructure.Repository.UsersRepository
{
    public interface IUserRepository
    {

        public List<User> GetUsers();

        public User GetUserById(int id);

        public User GetUserByUserName(string name);
        public bool AddUser(User user);

        //public bool UpdateUser(int id, User user);

        public bool UpdateUser(User user);

        public bool DeleteUser(int id);
    }
}
