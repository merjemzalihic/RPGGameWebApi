using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace RPGGame.Database
{
    public interface IDataContext
    {
        int SaveChanges();
        void Seed();
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        EntityEntry Attach(object entity);
        EntityEntry Entry(object entity);
        DatabaseFacade Database { get; }
    }
}
