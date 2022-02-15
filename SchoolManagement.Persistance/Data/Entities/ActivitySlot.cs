using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Persistance.Data.Entities
{
    public class ActivitySlot : BaseEntity
    {
        public string Slot { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public Activity Activity { get; set; }

    }
}
