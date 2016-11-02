using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePortal.DomainModel.Entities
{
    public class EntityBase
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }
    }
}
