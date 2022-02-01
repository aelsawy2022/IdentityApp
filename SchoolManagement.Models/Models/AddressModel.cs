using SchoolManagement.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Models.Models
{
    public class AddressModel : BaseModel
    {
        public string Desc { get; set; }
        public ICollection<SchoolModel> Schools { get; set; }
    }
}
