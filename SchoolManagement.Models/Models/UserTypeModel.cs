using SchoolManagement.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Models.Models
{
    public class UserTypeModel : BaseModel
    {
        public string Name { get; set; }
        public bool Active { get; set; }
        public bool IsSelected { get; set; }
        public ICollection<ClassUsersModel> ClassUsers { get; set; }
    }
}
