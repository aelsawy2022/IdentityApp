using SchoolManagement.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Models.Models
{
    public class SeasonModel : BaseModel
    {
        public string Name { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public bool Current { get; set; }
        public SchoolModel School { get; set; }
        public ICollection<ClassUsersModel> ClassUsers { get; set; }
    }
}
