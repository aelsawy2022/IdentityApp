using SchoolManagement.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Models.Models
{
    public class ActivityClassModel
    {
        public Guid ActivityId { get; set; }
        public ActivityModel Activity { get; set; }

        public Guid ClassId { get; set; }
        public ClassModel Class { get; set; }
    }
}
