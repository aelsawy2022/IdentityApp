using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Models.Models
{
    public class ActivityInstanceDetailModel : BaseModel
    {
        public Guid InstanceId { get; set; }
        public Guid UserId { get; set; }
        public string Note { get; set; }

        public ActivityInstanceModel ActivityInstance { get; set; }
        public UsersModel User { get; set; }
    }
}
