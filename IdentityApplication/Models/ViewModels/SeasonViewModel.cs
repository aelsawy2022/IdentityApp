using IdentityApplication.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApplication.Models.ViewModels
{
    public class SeasonViewModel
    {
        public Season Season { get; set; }
        public List<Season> Seasons { get; set; }
        public School School { get; set; }
    }
}
