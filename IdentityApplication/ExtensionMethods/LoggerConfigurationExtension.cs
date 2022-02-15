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
            return sinkConfiguration.Email(new EmailConnectionInfo()
            {
                FromEmail = "aelsawy2023@gmail.com",
                ToEmail = "ahmed_elsawy22@hotmail.com",
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
