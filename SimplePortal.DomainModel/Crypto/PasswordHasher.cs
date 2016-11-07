using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplePortal.DomainModel.Entities;

namespace SimplePortal.DomainModel.Crypto
{
    public class PasswordHasher : IPasswordHasher
    {
        public string ClearTextPassword { get; set; }

        public string HashedPassword
        {
            get { return Crypto.RNGCryptoServiceProviderPasswordManager.CreateHash(ClearTextPassword); }
        }


    }
}
