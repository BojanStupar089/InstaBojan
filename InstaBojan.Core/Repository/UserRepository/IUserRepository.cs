using InstaBojan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaBojan.Core.Repository.UserRepository
{
    public interface IUserRepository
    {
        public List<User> GetUsers();

        public User GetUserById(int id);

        public bool AddUser(User user);

        public bool UpdateUser(User user);

        public bool DeleteUser(int id);
    }
}
