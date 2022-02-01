using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Persistance.Data.Entities
{
    public class Grade : BaseEntity
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public int StudentsNo { get; set; }
        public int ClassesNo { get; set; }
        public int MaterialsNo { get; set; }
        public School School { get; set; }

        public ICollection<Class> Classes { get; set; }
    }
}
