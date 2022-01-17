using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Data.Entities
{
    public class Address : BaseEntity
    {
        public string Desc { get; set; }
        public ICollection<School> Schools { get; set; }
    }
}
