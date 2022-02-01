using System.Collections.Generic;

namespace SchoolManagement.Models.Models.ViewModels
{
    public class ManagementViewModel
    {
        public ManagementModel Management { get; set; }
        public List<ManagementModel> Managements { get; set; }
        public List<GovernorateModel> Governorates { get; set; }
    }
}
