using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Persistance.Data.Entities
{
    public class Management : BaseEntity
    {
        public string Name { get; set; }
        public Governorate Governorate { get; set; }
        public ICollection<School> Schools { get; set; }
    }
}
