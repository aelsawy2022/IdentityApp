using System.Collections.Generic;

namespace SchoolManagement.Models.Models.ViewModels
{
    public class SeasonViewModel
    {
        public SeasonModel Season { get; set; }
        public List<SeasonModel> Seasons { get; set; }
        public SchoolModel School { get; set; }
    }
}
