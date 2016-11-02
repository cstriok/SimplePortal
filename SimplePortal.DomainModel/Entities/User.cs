using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePortal.DomainModel.Entities
{
    public class User : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public UserRole Role { get; set; }

        public void HashPassword()
        {
            string hash = Crypto.RNGCryptoServiceProviderPasswordManager.CreateHash(Password);
            Password = hash;
        }

        public bool CheckPassword(string clearTextPassword)
        {
            return Crypto.RNGCryptoServiceProviderPasswordManager.VerifyPassword(clearTextPassword, Password);
        }
    }
}
