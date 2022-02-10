using Serilog;
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Sinks.Email;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace IdentityApplication.ExtensionMethods
{
    public static class LoggerConfigurationExtension
    {
        public static LoggerConfiguration EmailCustom(this LoggerSinkConfiguration sinkConfiguration, LogEventLevel restrictedToMinimumLevel)
        {
            List<string> toEmails = new List<string>();
            toEmails.Add("ahmed_elsawy22@hotmail.com");
            toEmails.Add("ahmed_elsawy16@hotmail.com");
            toEmails.Add("aabdelwahab@fcih1.com");

            //return sinkConfiguration.Email("ahmedragabelsawy13@gmail.com", toEmails, "smtp.gmail.com");

            return sinkConfiguration.Email(new EmailConnectionInfo()
            {
                FromEmail = "aelsawy2023@gmail.com",
                ToEmail = "ahmed_elsawy22@hotmail.com,ahmed_elsawy16@hotmail.com,aabdelwahab@fcih1.com",
                EnableSsl = true,
                IsBodyHtml = false,
                EmailSubject = "Error!!",
                MailServer = "smtp.gmail.com",
                NetworkCredentials = new NetworkCredential
                {
                    UserName = "aelsawy2023@gmail.com",
                    Password = "Test@12345"
                },
                Port = 465
            }, "[{Timestamp:HH:mm:ss.fff} {Level:u3}] {Message:lj} <s:{SourceContext}>{NewLine}{Exception}", restrictedToMinimumLevel, 10, null, null, "Error!!");
        }
    }
}
