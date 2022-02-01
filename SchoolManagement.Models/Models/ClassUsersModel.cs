using System;

namespace SchoolManagement.Models.Models
{
    public class ClassUsersModel
    {
        public Guid ClassId { get; set; }
        public ClassModel Class { get; set; }

        public string UserId { get; set; }
        public UsersModel User { get; set; }

        public Guid UserTypeId { get; set; }
        public UserTypeModel UserType { get; set; }

        public Guid SeasonId { get; set; }
        public SeasonModel Season { get; set; }
    }
}
