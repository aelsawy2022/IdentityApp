using System.Collections.Generic;
using SchoolManagement.Models.Models;

namespace SchoolManagement.ViewModels.ViewModels
{
    public class SeasonVM
    {
        public SeasonModel Season { get; set; }
        public List<SeasonModel> Seasons { get; set; }
        public SchoolModel School { get; set; }
    }
}
