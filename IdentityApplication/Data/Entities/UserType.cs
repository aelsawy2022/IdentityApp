using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Data.Entities
{
    public class UserType : BaseEntity
    {
        public string Name { get; set; }
        public bool Active { get; set; }
        public ICollection<ClassUser> ClassUsers { get; set; }
    }
}
