﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Persistance.Data.Entities
{
    public class ActivitySlot
    {
        public string Slot { get; set; }
        public Activity Activity { get; set; }

    }
}