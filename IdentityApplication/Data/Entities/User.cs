using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Data.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public bool Active { get; set; }

        public Governorate Governorate { get; set; }
        public ICollection<ClassUser> ClassUsers { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
