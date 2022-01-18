using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Data.Entities
{
    public class School : BaseEntity
    {
        public string Name { get; set; }
        public Address Address { get; set; }
        public Management Management { get; set; }
        public ICollection<Grade> Grades { get; set; }
        public ICollection<Activity> Activities { get; set; }
        public ICollection<Season> Seasons { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}
