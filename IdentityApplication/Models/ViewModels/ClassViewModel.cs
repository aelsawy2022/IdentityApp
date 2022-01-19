using IdentityApplication.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Models.ViewModels
{
    public class ClassViewModel
    {
        public Class Class { get; set; }
        public List<Class> Classes { get; set; }
        public List<Grade> Grades { get; set; }
        public Grade Grade { get; set; }
        public Guid SchoolId { get; set; }
    }
}
