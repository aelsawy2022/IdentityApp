using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Data.Entities
{
    public class ActivityUserType
    {
        public Guid UserTypeId { get; set; }
        public UserType UserType { get; set; }

        public Guid ActivityId { get; set; }
        public Activity Activity { get; set; }
    }
}
