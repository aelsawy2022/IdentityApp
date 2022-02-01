using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Persistance.Data.Entities
{
    public class ActivityClass
    {
        public Guid ActivityId { get; set; }
        public Activity Activity { get; set; }

        public Guid ClassId { get; set; }
        public Class Class { get; set; }
    }
}
