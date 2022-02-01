using SchoolManagement.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Models.Models
{
    public class ActivityUserTypeModel
    {
        public Guid UserTypeId { get; set; }
        public UserTypeModel UserType { get; set; }

        public Guid ActivityId { get; set; }
        public ActivityModel Activity { get; set; }
    }
}
