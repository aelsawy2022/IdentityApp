using System.Collections.Generic;

namespace SchoolManagement.Models.Models.ViewModels
{
    public class GradeViewModel
    {
        public GradeModel Grade { get; set; }
        public List<GradeModel> Grades { get; set; }
        public SchoolModel School { get; set; }
    }
}
