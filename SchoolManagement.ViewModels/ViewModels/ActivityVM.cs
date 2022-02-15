using SchoolManagement.Models.Models;
using System.Collections.Generic;

namespace SchoolManagement.ViewModels.ViewModels
{
    public class ActivityVM
    {
        public ActivityModel Activity { get; set; }
        public List<ActivityModel> Activities { get; set; }
        public SchoolModel School { get; set; }
        public List<UserTypeModel> UserTypes { get; set; }
        public List<GradeModel> Grades { get; set; }
        public List<ActivitySlotModel> ActivitySlots { get; set; }
        public ActivitySlotModel Slot { get; set; }

    }
}
