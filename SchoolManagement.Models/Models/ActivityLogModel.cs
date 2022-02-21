using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Models.Models
{
    public class ActivityLogModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime CreationDate { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Url { get; set; }
        public string IPAddress { get; set; }
    }
}
