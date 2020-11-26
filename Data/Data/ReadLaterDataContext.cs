using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using Microsoft.AspNet.Identity.EntityFramework;
using ReadLater.Entities;

namespace ReadLater.Data
{
    public class ReadLaterDataContext : IdentityDbContext<User>, IDbContext
    {
        static ReadLaterDataContext()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ReadLaterDataContext>());
        }

        public ReadLaterDataContext()
            : base("Name=ReadLaterDataContext", throwIfV1Schema: false)
        {
        }

        public static ReadLaterDataContext Create()
        {
            return new ReadLaterDataContext();
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public override int SaveChanges()
        {
            this.ApplyStateChanges();
            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            EntityTypeConfiguration<Category> categoryMap = modelBuilder.Entity<Category>();
            EntityTypeConfiguration<Bookmark> bookmarkMap = modelBuilder.Entity<Bookmark>();

            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<ReadLater.Entities.Category> Categories { get; set; }
        public override IDbSet<User> Users { get; set; }
    }
}
