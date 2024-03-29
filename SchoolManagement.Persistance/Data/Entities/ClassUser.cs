﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Persistance.Data.Entities
{
    public class ClassUser
    {
        public Guid ClassId { get; set; }
        public Class Class { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid UserTypeId { get; set; }
        public UserType UserType { get; set; }

        public Guid SeasonId { get; set; }
        public Season Season { get; set; }
    }
}
