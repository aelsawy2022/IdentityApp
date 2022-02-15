using System;
using System.Collections.Generic;

namespace SchoolManagement.Models.Models
{
    public class ActivityModel
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public bool Active { get; set; }
        public SchoolModel School { get; set; }
        public List<RolesModel> Roles { get; set; }
        public List<ActivityClassModel> Classes { get; set; }
        public List<ActivitySlotModel> Slots { get; set; }
        public List<ActivityUserTypeModel> UserTypes { get; set; }
        public bool IsSelected { get; set; }
    }
}
