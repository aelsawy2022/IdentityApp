using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Models.Models
{
    public class ErrorLogModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string MessageTemplate { get; set; }
        public string Level { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Exception { get; set; }
        public string Properties { get; set; }
    }

    public class ErrorLogFilter : PagingModel
    {
        public DateTime? TimeStamp { get; set; }
    }
}
