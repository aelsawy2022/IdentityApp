using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Persistance.Data.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public bool Active { get; set; }
        public School School { get; set; }
        public Activity Activity { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
