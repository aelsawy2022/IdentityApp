using System;

namespace SchoolManagement.Models.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public string ErrorMessage { get; set; }
        public bool ShowErrorMessage => !string.IsNullOrEmpty(ErrorMessage);
    }
}
