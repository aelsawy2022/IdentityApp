﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Persistance.Data.Entities
{
    public class Activity : BaseEntity
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public bool Active { get; set; }
        public School School { get; set; }
        public ICollection<Role> Roles { get; set; }
        public ICollection<ActivityClass> Classes { get; set; }
        public ICollection<ActivitySlot> Slots { get; set; }
        public ICollection<ActivityUserType> UserTypes { get; set; }
    }
}
