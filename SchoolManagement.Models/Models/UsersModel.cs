using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Models.Models
{
    public class UsersModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public bool Active { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        [PersonalData]
        public bool TwoFactorEnabled { get; set; }
        [PersonalData]
        public bool PhoneNumberConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string SecurityStamp { get; set; }
        public string PasswordHash { get; set; }
        public bool EmailConfirmed { get; set; }
        public string NormalizedEmail { get; set; }
        public string Email { get; set; }
        public string NormalizedUserName { get; set; }
        public string UserName { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsSuperAdmin { get; set; }
        public IFormFile ImageFile { get; set; }
        public bool IsSelected { get; set; }

        public GovernorateModel Governorate { get; set; }
        public ICollection<ClassUsersModel> ClassUsers { get; set; }
        public List<RolesModel> Roles { get; set; }
    }
}
