using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Persistance.Data.Entities
{
    public class Governorate : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Management> Managements { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
