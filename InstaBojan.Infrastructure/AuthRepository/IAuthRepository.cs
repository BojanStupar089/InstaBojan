using InstaBojan.Core.Security;

namespace InstaBojan.Infrastructure.AuthRepository
{
    public interface IAuthRepository
    {
        string Login(Login login);
        void Register(Register register);
    }
}
