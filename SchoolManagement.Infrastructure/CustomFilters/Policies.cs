using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Infrastructure.CustomFilters
{
    public class Policies
    {
        public const string GeneralSuperAdmin = "GeneralSuperAdmin";
        public const string GeneralSuperAdmin_SchoolAdmin = "GeneralSuperAdmin_SchoolAdmin";
        public const string GeneralAdmin = "GeneralAdmin";
        public const string GeneralAdmin_SchoolAdmin = "GeneralAdmin_SchoolAdmin";
    }

    public class Roles
    {
        public const string SUPER_ADMIN = "SuperAdmin";
        public const string ADMIN = "Admin";
        public const string SCHOOL_ADMIN = "SchoolAdmin";
        public const string SCHOOL_SUPER_ADMIN = "SchoolSuperAdmin";
        public const string SCHOOL_ACTIVITY = "SchoolActivity";
    }
}
