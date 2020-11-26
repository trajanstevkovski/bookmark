using ReadLater.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace ReadLater.Data
{
    public interface IDbContext
    {
        IDbSet<User> Users { get; set; }
        IDbSet<T> Set<T>() where T : class;
        int SaveChanges();
        DbEntityEntry Entry(object o);
        void Dispose();
    }
}
