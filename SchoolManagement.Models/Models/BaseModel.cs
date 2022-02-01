using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Models.Models
{
    public class BaseModel
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
