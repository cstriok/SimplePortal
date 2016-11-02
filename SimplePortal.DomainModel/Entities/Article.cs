using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePortal.DomainModel.Entities
{
    public class Article : EntityBase
    {
        public string Headline { get; set; }
        public string Body { get; set; }
        public Author Author { get; set; }
        public List<string> Tags { get; set; }
    }
}
