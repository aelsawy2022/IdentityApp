using SchoolManagement.Models.Models;
using System.Collections.Generic;

namespace SchoolManagement.ViewModels.ViewModels
{
    public class SchoolVM
    {
        public SchoolModel School { get; set; }
        public List<ManagementModel> Managements { get; set; }
    }
}
