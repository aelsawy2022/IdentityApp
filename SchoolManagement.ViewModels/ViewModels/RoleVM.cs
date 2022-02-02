using SchoolManagement.Models.Models;
using System.Collections.Generic;

namespace SchoolManagement.ViewModels.ViewModels
{
    public class RoleVM
    {
        public RolesModel Role { get; set; }
        public List<RolesModel> Roles { get; set; }
        public SchoolModel School { get; set; }
    }
}
