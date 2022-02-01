using System;
using System.Collections.Generic;

namespace SchoolManagement.Models.Models
{
    public class SchoolModel
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Name { get; set; }
        public AddressModel Address { get; set; }
        public ManagementModel Management { get; set; }
        public List<RolesModel> Roles { get; set; }
        public List<ActivityModel> Activities { get; set; }
        public bool IsSelected { get; set; }
        public bool IsActivitiesSelected { get; set; }
    }
}
