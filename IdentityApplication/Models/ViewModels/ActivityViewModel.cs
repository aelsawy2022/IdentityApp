using IdentityApplication.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Models.ViewModels
{
    public class ActivityViewModel
    {
        public Activity Activity { get; set; }
        public List<School> Schools { get; set; }
    }
}
