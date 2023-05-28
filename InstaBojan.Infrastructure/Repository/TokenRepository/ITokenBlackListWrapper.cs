namespace InstaBojan.Infrastructure.Repository.TokenRepository
{
    public interface ITokenBlackListWrapper
    {

        void AddToBlackList(string token);
    }
}
