using ReadLater.Entities;

namespace ReadLater.Repository
{
    public interface IUserRepository
    {
        User GetUser(string email);
    }
}
