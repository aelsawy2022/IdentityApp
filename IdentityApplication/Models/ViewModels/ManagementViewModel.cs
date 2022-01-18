using IdentityApplication.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Models.ViewModels
{
    public class ManagementViewModel
    {
        public Management Management { get; set; }
        public List<Management> Managements { get; set; }
        public List<Governorate> Governorates { get; set; }
    }
}
