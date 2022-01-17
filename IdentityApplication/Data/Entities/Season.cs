using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Data.Entities
{
    public class Season : BaseEntity
    {
        public string Name { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public bool Current { get; set; }
        public School School { get; set; }
        public ICollection<ClassUser> ClassUsers { get; set; }
    }
}
