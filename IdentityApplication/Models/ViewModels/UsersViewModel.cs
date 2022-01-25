using IdentityApplication.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Models.ViewModels
{
    public class UsersViewModel : PagingModel
    {
        public List<UsersModel> Users { get; set; }
        public UsersModel User { get; set; }
        public List<Governorate> Governorates { get; set; }

        public List<RolesModel> Roles { get; set; }
        public List<SchoolModel> Schools { get; set; }
        public List<ActivityModel> Activities { get; set; }

        public bool IsGeneralRolesSelected { get; set; }
        public bool IsSchoolsRolesSelected { get; set; }
    }

    public class UsersFilter : PagingModel
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public Guid ClassId { get; set; }

    }
}
