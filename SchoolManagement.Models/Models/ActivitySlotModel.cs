using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Models.Models
{
    public class ActivitySlotModel : BaseModel
    {
        public string Slot { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public ActivityModel Activity { get; set; }
    }
}
