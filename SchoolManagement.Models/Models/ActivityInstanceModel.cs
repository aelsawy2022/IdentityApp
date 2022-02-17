using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Models.Models
{
    public class ActivityInstanceModel : BaseModel
    {
        public string Name { get; set; }
        public Guid ActivityId { get; set; }
        public Guid SeasonId { get; set; }
        public DateTime InstanceDate { get; set; }
        public bool IsLocked { get; set; }

        public ActivityModel Activity { get; set; }
        public SeasonModel Season { get; set; }
    }
}
