using SchoolManagement.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Models.Models
{
    public class GradeModel : BaseModel
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public int StudentsNo { get; set; }
        public int ClassesNo { get; set; }
        public int MaterialsNo { get; set; }
        public SchoolModel School { get; set; }

        public ICollection<ClassModel> Classes { get; set; }
    }
}
