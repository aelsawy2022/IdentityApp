using IdentityApplication.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Models.ViewModels
{
    public class RoleViewModel
    {
        public Role Role { get; set; }
        public List<Role> Roles { get; set; }
        public School School { get; set; }
    }
}
