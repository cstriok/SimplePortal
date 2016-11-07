using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePortal.DomainModel.Crypto
{
    public class PasswordChecker : IPasswordChecker
    {
        public string ClearTextPassword { get; set; }

        public string HashedPassword { get; set; }

        public bool PasswordCheckOk
        {
            get {  return Crypto.RNGCryptoServiceProviderPasswordManager.VerifyPassword(ClearTextPassword, HashedPassword); }
        }
    }
}
