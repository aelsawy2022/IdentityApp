using SchoolManagement.Models.Models;
using System.Collections.Generic;

namespace SchoolManagement.ViewModels.ViewModels
{
    public class ActivityVM
    {
        public ActivityModel Activity { get; set; }
        public List<ActivityModel> Activities { get; set; }
        public SchoolModel School { get; set; }
    }
}
