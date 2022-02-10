using Serilog;
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Sinks.Email;
using System;
using System.Net;

namespace IdentityApplication.ExtensionMethods
{
    public static class LoggerConfigurationExtension
    {
        public static LoggerConfiguration EmailCustom(this LoggerSinkConfiguration sinkConfiguration, LogEventLevel restrictedToMinimumLevel)
        {
            return sinkConfiguration.Email(new EmailConnectionInfo()
            {
                FromEmail = "",
                ToEmail = "",
                EnableSsl = true,
                EmailSubject = "Error!!",
                MailServer = "smtp.gmail.com",
                NetworkCredentials = new NetworkCredential
                {
                    UserName = "",
                    Password = ""
                },
                Port = 587
            }, "[{Timestamp:HH:mm:ss.fff} {Level:u3}] {Message:lj} <s:{SourceContext}>{NewLine}{Exception}", LogEventLevel.Warning, 10, TimeSpan.FromMinutes(5), null, "Error!!");
        }
    }
}
