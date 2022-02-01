using System.Collections.Generic;

namespace SchoolManagement.Models.Models.ViewModels
{
    public class SchoolViewModel
    {
        public SchoolModel School { get; set; }
        public List<ManagementModel> Managements { get; set; }
    }
}
