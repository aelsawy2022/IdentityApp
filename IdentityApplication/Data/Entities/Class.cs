using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Data.Entities
{
    public class Class : BaseEntity
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public string StudentImg { get; set; }
        public string TeacherImg { get; set; }
        public bool Active { get; set; }
        public int StudenstNo { get; set; }
        public int MaterilasNo { get; set; }
        public Grade Grade { get; set; }
        public ICollection<ClassUser> ClassUsers { get; set; }
        //public ICollection<ActivityClass> Activities { get; set; }
    }
}
