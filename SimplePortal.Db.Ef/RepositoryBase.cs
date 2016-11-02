using SimplePortal.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePortal.Db.Ef
{
    public class RepositoryBase<T> where T : EntityBase, new()
    {
        protected readonly SimplePortalEfDbContext _dbContext = null;

        public RepositoryBase()
        {
            _dbContext = new SimplePortalEfDbContext();
        }

        protected bool RecordHasIdAndUid(T entity)
        {
            return entity.Id != 0 && !entity.Uid.Equals(Guid.Empty);
        }
        
        public virtual T FindRecord(Guid uid)
        {
            return _dbContext.Set<T>().FirstOrDefault(e => e.Uid.Equals(uid));
        }


    }
}
