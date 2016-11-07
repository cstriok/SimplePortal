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
        public UserRepository(ISimplePortalEfDbContext context)
            : base(context)
        {

        }

        public List<User> Items
        {
            get
            {
                return new List<User>(_dbContext.User);
            }
        }

        /// <summary>
        /// throws RecrodNotNewException if record has id != 0 or uid != guid.empty
        /// </summary>
        /// <param name="entity">new record</param>
        public void Create(User entity)
        {
            if (!RecordHasIdAndUid(entity))
            {
                entity.Uid = Guid.NewGuid();
                _dbContext.User.Add(entity);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new RecordNotNewException();
            }
        }

        /// <summary>
        /// throws RecordNotFoundInDbException if record with uid does not exist in db
        /// </summary>
        /// <param name="entity">existing record</param>
        public void Delete(User entity)
        {
            User userInDb = FindRecord(entity.Uid);
            
            if (userInDb != null)
            {
                _dbContext.User.Remove(userInDb);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new RecordNotFoundInDbException();
            }

        }

        /// <summary>
        /// throws RecordNotFoundInDbException if record with uid does not exist in db
        /// </summary>
        /// <param name="entity">existing record</param>
        public void Update(User entity)
        {
            User userInDb = FindRecord(entity.Uid);

            if (userInDb != null)
            {
                userInDb.FirstName = entity.FirstName;
                userInDb.LastName = entity.LastName;
                userInDb.Login = entity.Login;
                userInDb.Mail = entity.Mail;
                userInDb.Password = entity.Password;
                userInDb.Role = entity.Role;
                _dbContext.SaveChanges();
            }
            else
            {
                throw new RecordNotFoundInDbException();
            }
        }
    }
}
