using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Data.Entities
{
    public class Class : BaseEntity
    {
        public int StudenstNo { get; set; }
        public int MaterilasNo { get; set; }
        public Grade Grade { get; set; }
        public ICollection<ClassUser> ClassUsers { get; set; }
        //public ICollection<ActivityClass> Activities { get; set; }
    }
}
