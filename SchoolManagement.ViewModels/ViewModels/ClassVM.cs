using System;
using System.Collections.Generic;
using SchoolManagement.Models.Models;

namespace SchoolManagement.ViewModels.ViewModels
{
    public class ClassVM
    {
        public ClassModel Class { get; set; }
        public List<ClassModel> Classes { get; set; }
        public List<GradeModel> Grades { get; set; }
        public GradeModel Grade { get; set; }
        public Guid SchoolId { get; set; }
    }
}
