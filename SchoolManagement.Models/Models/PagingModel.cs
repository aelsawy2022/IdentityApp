using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Models.Models
{
    public class PagingModel
    {
        public int CurrentPage { get; set; } = 1;
        public int MaxRows { get; set; } = 2;
        public int PageCount { get; set; }
    }
}
