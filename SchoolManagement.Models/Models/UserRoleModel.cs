using SchoolManagement.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Models.Models
{
    public class UserRoleModel
    {
        public virtual UsersModel User { get; set; }

        public virtual RolesModel Role { get; set; }
    }
}
