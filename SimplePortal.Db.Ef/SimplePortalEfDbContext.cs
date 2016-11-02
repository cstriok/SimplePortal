using SimplePortal.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePortal.Db.Ef
{
    public class SimplePortalEfDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
