using SchoolManagement.Models.Models;
using System.Collections.Generic;

namespace SchoolManagement.ViewModels.ViewModels
{
    public class GradeVM
    {
        public GradeModel Grade { get; set; }
        public List<GradeModel> Grades { get; set; }
        public SchoolModel School { get; set; }
    }
}
