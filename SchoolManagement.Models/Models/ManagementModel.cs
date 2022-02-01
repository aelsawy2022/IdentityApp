using SchoolManagement.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Models.Models
{
    public class ManagementModel : BaseModel
    {
        public string Name { get; set; }
        public GovernorateModel Governorate { get; set; }
        public ICollection<SchoolModel> Schools { get; set; }
    }
}
