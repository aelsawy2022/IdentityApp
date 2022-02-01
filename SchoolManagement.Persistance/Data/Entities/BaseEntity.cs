using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Persistance.Data.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        //[MaxLength(200)]
        //[Required]
        //public string Name { get; set; }
        //[MaxLength(2000)]
        //public string Desc { get; set; }
        //public bool Active { get; set; }
    }
}
