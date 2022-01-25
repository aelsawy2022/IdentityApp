using IdentityApplication.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Models
{
    public class SchoolModel
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public Management Management { get; set; }
        public List<RolesModel> Roles { get; set; }
        public List<ActivityModel> Activities { get; set; }
        public bool IsSelected { get; set; }
        public bool IsActivitiesSelected { get; set; }
    }
}
