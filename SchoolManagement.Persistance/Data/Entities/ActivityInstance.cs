using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Persistance.Data.Entities
{
    public class ActivityInstance : BaseEntity
    {
        public string Name { get; set; }
        public Guid ActivityId { get; set; }
        public Guid SeasonId { get; set; }
        public DateTime InstanceDate { get; set; }
        public bool IsLocked { get; set; }

        public Activity Activity { get; set; }
        public Season Season { get; set; }
    }
}
