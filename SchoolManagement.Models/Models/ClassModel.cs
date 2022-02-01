using SchoolManagement.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Models.Models
{
    public class ClassModel : BaseModel
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public string StudentImg { get; set; }
        public string TeacherImg { get; set; }
        public bool Active { get; set; }
        public int StudenstNo { get; set; }
        public int MaterilasNo { get; set; }
        public GradeModel Grade { get; set; }
        public ICollection<ClassUsersModel> ClassUsers { get; set; }
    }
}
