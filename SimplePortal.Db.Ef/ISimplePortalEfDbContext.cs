using SimplePortal.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimplePortal.Db.Ef
{
    public interface ISimplePortalEfDbContext : IDisposable
    {
        // db tables 
        DbSet<User> User { get; set; }
        
        // must re-reference db context mehthods
        DbChangeTracker ChangeTracker { get; }
        DbContextConfiguration Configuration { get; }
        Database Database { get; }
        DbEntityEntry Entry(object entity);
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        IEnumerable<DbEntityValidationResult> GetValidationErrors();
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        DbSet Set(Type entityType);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

    }
}
