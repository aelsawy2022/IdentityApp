using SchoolManagement.Models.Models;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Models.Models.ViewModels
{
    public class ClassUserViewModel
    {
        public List<ClassUsersModel> ClassUsers { get; set; }
        public List<UsersModel> AllUsers { get; set; }
        public List<UserTypeModel> Types { get; set; }
        public Guid SelectedTypeId { get; set; }
        public ClassModel Class { get; set; }
        public Guid SchoolId { get; set; }
        public Guid GradeId { get; set; }
        public UsersFilter usersFilter { get; set; }
    }
}
