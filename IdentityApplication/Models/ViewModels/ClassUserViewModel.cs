using IdentityApplication.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Models.ViewModels
{
    public class ClassUserViewModel
    {
        public List<ClassUsersModel> ClassUsers { get; set; }
        public List<UsersModel> AllUsers { get; set; }
        public List<UserType> Types { get; set; }
        public Guid SelectedTypeId { get; set; }
        public Class Class { get; set; }
        public Guid SchoolId { get; set; }
        public Guid GradeId { get; set; }
        public UsersFilter usersFilter { get; set; }
    }
}
