using ReadLater.Data;
using ReadLater.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ReadLater.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbContext _dbContext;

        public UserRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User GetUser(string email)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Email == email);
        }
    }
}
