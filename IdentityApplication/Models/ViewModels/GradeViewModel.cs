using IdentityApplication.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Models.ViewModels
{
    public class GradeViewModel
    {
        public Grade Grade { get; set; }
        public List<School> Schools { get; set; }
    }
}
