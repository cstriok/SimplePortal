using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePortal.DomainModel.Crypto
{
    public interface IPasswordHasher
    {
        string HashedPassword { get; }
        string ClearTextPassword { get; set; }
    }
}
