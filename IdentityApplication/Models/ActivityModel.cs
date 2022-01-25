using IdentityApplication.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Models
{
    public class ActivityModel
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public bool Active { get; set; }
        public School School { get; set; }
        public List<RolesModel> Roles { get; set; }

        public bool IsSelected { get; set; }
    }
}
