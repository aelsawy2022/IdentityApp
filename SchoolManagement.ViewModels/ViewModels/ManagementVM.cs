using SchoolManagement.Models.Models;
using System.Collections.Generic;

namespace SchoolManagement.ViewModels.ViewModels
{
    public class ManagementVM
    {
        public ManagementModel Management { get; set; }
        public List<ManagementModel> Managements { get; set; }
        public List<GovernorateModel> Governorates { get; set; }
    }
}
