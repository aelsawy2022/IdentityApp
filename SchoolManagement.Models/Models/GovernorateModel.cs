using SchoolManagement.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Models.Models
{
    public class GovernorateModel : BaseModel
    {
        public string Name { get; set; }
        public ICollection<ManagementModel> Managements { get; set; }
        public ICollection<UsersModel> Users { get; set; }
    }
}
