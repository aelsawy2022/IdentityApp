using SchoolManagement.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.ViewModels.ViewModels
{
    public class ErrorLogVM
    {
        public ErrorLogFilter ErrorLogFilter { get; set; }
        public List<ErrorLogModel> ErrorLogs { get; set; }
    }
}
