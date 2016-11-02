using SimplePortal.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePortal.Db.Ef
{
    public interface IEfRepository<T> where T : EntityBase, new()
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        List<T> Items { get; }
    }
}
