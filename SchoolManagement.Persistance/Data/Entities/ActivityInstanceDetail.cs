using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Persistance.Data.Entities
{
    public class ActivityInstanceDetail : BaseEntity
    {
        public Guid InstanceId { get; set; }
        public Guid UserId { get; set; }
        public string Note { get; set; }

        public ActivityInstance ActivityInstance { get; set; }
        public User User { get; set; }
    }
}
