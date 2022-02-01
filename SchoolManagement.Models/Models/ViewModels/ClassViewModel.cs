using System;
using System.Collections.Generic;

namespace SchoolManagement.Models.Models.ViewModels
{
    public class ClassViewModel
    {
        public ClassModel Class { get; set; }
        public List<ClassModel> Classes { get; set; }
        public List<GradeModel> Grades { get; set; }
        public GradeModel Grade { get; set; }
        public Guid SchoolId { get; set; }
    }
}
