using InstaBojan.Core.Models;

namespace InstaBojan.Infrastructure.Repository.UsersRepository
{
    public interface IUserRepository
    {

        List<User> GetUsers();
        User GetUserById(int id);
        User GetUserByUserName(string name);
        User GetUserByEmail(string email);
        //public bool AddUser(User user);
        bool UpdateUser(int id, User user);
        bool DeleteUser(int id);



    }
}
