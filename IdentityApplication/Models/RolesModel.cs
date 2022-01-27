using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Models
{
    public class RolesModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }
        public bool IsSelected { get; set; }

        public SchoolModel School { get; set; }
        public ActivityModel Activity { get; set; }
    }
}
