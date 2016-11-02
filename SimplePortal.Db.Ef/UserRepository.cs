using SimplePortal.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePortal.Db.Ef
{
    public class UserRepository : RepositoryBase<User>, IEfRepository<User>
    {
        public List<User> Items
        {
            get
            {
                return new List<User>(_dbContext.Users);
            }
        }

        public void Create(User entity)
        {
            if (!RecordHasIdAndUid(entity))
            {
                entity.Uid = Guid.NewGuid();
                _dbContext.Users.Add(entity);
                _dbContext.SaveChanges();
            }
        }

        public void Delete(User entity)
        {
            User userInDb = FindRecord(entity.Uid);
            
            if (userInDb != null)
            {
                _dbContext.Users.Remove(userInDb);
                _dbContext.SaveChanges();
            }
        }

        public void Update(User entity)
        {
            User userInDb = FindRecord(entity.Uid);

            if (userInDb != null)
            {
                userInDb.LastName = entity.LastName;
                userInDb.Login = entity.Login;
                userInDb.Mail = entity.Mail;
                userInDb.Password = entity.Password;
                userInDb.Role = entity.Role;
            }
        }
    }
}
