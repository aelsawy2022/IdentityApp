using System;
using System.Collections.Generic;
using SchoolManagement.Models.Models;

namespace SchoolManagement.ViewModels.ViewModels
{
    public class UserVM : PagingModel
    {
        public List<UsersModel> Users { get; set; }
        public UsersModel User { get; set; }
        public List<GovernorateModel> Governorates { get; set; }

        public List<RolesModel> Roles { get; set; }
        public List<SchoolModel> Schools { get; set; }
        public List<ActivityModel> Activities { get; set; }

        public bool IsGeneralRolesSelected { get; set; }
        public bool IsSchoolsRolesSelected { get; set; }
        public string ServerName { get; set; } = "@school.com";
    }

    public class UsersFilter : PagingModel
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public Guid ClassId { get; set; }

    }
}
