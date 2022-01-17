using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Data.Entities
{
    public class Role : IdentityRole
    {
        public bool Active { get; set; }
        public School School { get; set; }
        public Activity Activity { get; set; }
    }
}
