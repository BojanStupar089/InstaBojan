using InstaBojan.Core.Models;

namespace InstaBojan.Infrastructure.Repository.TokenRepository
{
    public class TokenBlackListWrapper : ITokenBlackListWrapper
    {
        public void AddToBlackList(string token)
        {
            TokenBlackList.AddToBlackList(token);
        }
    }
}
