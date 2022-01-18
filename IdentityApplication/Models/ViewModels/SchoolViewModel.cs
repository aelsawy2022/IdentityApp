using IdentityApplication.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Models.ViewModels
{
    public class SchoolViewModel
    {
        public School School { get; set; }
        public List<Management> Managements { get; set; }
    }
}
