using SchoolManagement.Models.Models;
using System.Collections.Generic;

namespace SchoolManagement.Models.Models.ViewModels
{
    public class ActivityViewModel
    {
        public ActivityModel Activity { get; set; }
        public List<ActivityModel> Activities { get; set; }
        public SchoolModel School { get; set; }
    }
}
