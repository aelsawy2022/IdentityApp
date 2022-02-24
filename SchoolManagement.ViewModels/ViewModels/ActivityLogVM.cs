using SchoolManagement.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.ViewModels.ViewModels
{
    public class ActivityLogVM
    {
        public ActivityLogFilter ActivityLogFilter { get; set; }
        public List<ActivityLogModel> ActivityLogs { get; set; }
    }
}
