using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Data.Entities
{
    public class Governorate : BaseEntity
    {
        public ICollection<Management> Managements { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
