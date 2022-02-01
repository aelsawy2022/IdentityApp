using System.Collections.Generic;

namespace SchoolManagement.Models.Models.ViewModels
{
    public class RoleViewModel
    {
        public RolesModel Role { get; set; }
        public List<RolesModel> Roles { get; set; }
        public SchoolModel School { get; set; }
    }
}
