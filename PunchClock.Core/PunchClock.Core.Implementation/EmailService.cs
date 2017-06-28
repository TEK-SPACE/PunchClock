using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using PunchClock.Configuration.Contract;
using PunchClock.Configuration.Model.Constants;
using PunchClock.Configuration.Service;
using PunchClock.Core.Contracts;
using PunchClock.Domain.Model;
using PunchClock.Domain.Model.Enum;
using PunchClock.View.Model;
using RedandBlue.Common;
using RedandBlue.Common.Logging;
using RedandBlue.Common.Track;

namespace PunchClock.Core.Implementation
{
    public class EmailService : IEmail
    {
        private readonly IAppSetting _appSettingService = new AppSettingService();
        private readonly IGeo _geoService = new GeoService();
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> All { get; }
        public IQueryable<User> AllIncluding(params Expression<Func<User, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public User Find(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(User entity)
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }

        public void InsertOrUpdate(User entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public string ComposeContactEmail(ContactView contact, GeoLocation geo)
        {
            throw new NotImplementedException();
        }


        public bool SendEmail(string msgBody, string msgSubject, string[] recipients, bool includeGeo)
        {
            var appSettings =_appSettingService.GetByModule(moduleId: (int) ModuleType.Core);

            MailMessage msg = new MailMessage
            {
                //ConfigurationManager.AppSettings["EmailFromTitle"]
                From = new MailAddress(
                    appSettings.First(x => x.Key.Equals(AppKey.CoreEmailFrom, StringComparison.OrdinalIgnoreCase)).Value,
                    appSettings.First(x => x.Key.Equals(AppKey.CoreEmailFromTitle, StringComparison.OrdinalIgnoreCase)).Value),
                Subject = msgSubject,
                Body = msgBody,
                IsBodyHtml = true
            };

            if (includeGeo)
            {
                msg.Body += GetGeoInfo();
            }

            foreach (var recipient in recipients)
            {
                msg.To.Add(recipient);
            }
            if (Convert.ToBoolean(appSettings
                .First(x => x.Key.Equals(AppKey.CoreNotifyingEnabled, StringComparison.OrdinalIgnoreCase)).Value))
            {
                foreach (var recipient in appSettings
                    .First(x => x.Key.Equals(AppKey.CoreNotifyingList, StringComparison.OrdinalIgnoreCase)).Value.Split(','))
                {
                    msg.Bcc.Add(recipient);
                }
            }

            SmtpClient client = new SmtpClient
            {
                UseDefaultCredentials = false,
                Host = appSettings.First(x => x.Key.Equals(AppKey.CoreSmtpHost, StringComparison.OrdinalIgnoreCase)).Value,
                Port = Convert.ToInt32(appSettings.First(x => x.Key.Equals("CoreSmtpPort", StringComparison.OrdinalIgnoreCase)).Value),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials =
                    new NetworkCredential(
                        appSettings.First(x => x.Key.Equals(AppKey.CoreEmailFrom, StringComparison.OrdinalIgnoreCase)).Value,
                        appSettings.First(x => x.Key.Equals(AppKey.CoreEmailPassword, StringComparison.OrdinalIgnoreCase)).Value),
                Timeout = 20000
            };

            try
            {
                client.Send(msg);
            }
            finally
            {
                msg.Dispose();
            }
            return true;
        }

        private string GetGeoInfo()
        {
            var geoTemplatePath = Path.Combine(Util.AssemblyDirectory, "Templates", "Email",
                ConfigurationManager.AppSettings["EmailTemplateGeo"]);
            if (!File.Exists(geoTemplatePath))
            {
                Log.Error($"Template doesnt't exists at {geoTemplatePath}");
            }
            else
            {
                var ipObject = _geoService.GetGeoLocation(ClientEnvironment.GetClientIp());

                var geoContent = File.ReadAllText(geoTemplatePath);
                geoContent = geoContent.Replace("#City#", ipObject.City);
                geoContent = geoContent.Replace("#Country#", ipObject.Country);
                geoContent = geoContent.Replace("#Isp#", ipObject.Isp);
                geoContent = geoContent.Replace("#Timezone#", ipObject.Timezone);
                geoContent = geoContent.Replace("#Zip#", ipObject.Zip);
                geoContent = geoContent.Replace("#IpAddress#", ipObject.IpAddress);
                return geoContent;
            }
            return string.Empty;
        }
    }
}
