using System;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using PunchClock.Configuration.Contract;
using PunchClock.Configuration.Service;
using PunchClock.Core.Contracts;
using PunchClock.Core.Models.Common;
using PunchClock.Core.Models.Common.Enum;
using PunchClock.Domain.Model;
using PunchClock.View.Model;

namespace PunchClock.Core.Implementation
{
    public class EmailService : IEmailRepository
    {
        private readonly IAppSetting _appSettingService = new AppSettingService();
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

        public string ComposeContactEmail(ContactView contact, GeoPlugin geo)
        {
            throw new NotImplementedException();
        }


        public bool SendEmail(string msgBody, string msgSubject, string[] recipients)
        {
            var appSettings =_appSettingService.GetByModule(moduleId: (int) ModuleType.Core);

            MailMessage msg = new MailMessage
            {
                //ConfigurationManager.AppSettings["EmailFromTitle"]
                From = new MailAddress(
                    appSettings.First(x => x.Key.Equals("CoreEmailFrom", StringComparison.OrdinalIgnoreCase)).Value,
                    appSettings.First(x => x.Key.Equals("CoreEmailFromTitle", StringComparison.OrdinalIgnoreCase)).Value),
                Subject = msgSubject,
                Body = msgBody,
                IsBodyHtml = true
            };
            foreach (var recipient in recipients)
            {
                msg.To.Add(recipient);
            }
            if (Convert.ToBoolean(appSettings
                .First(x => x.Key.Equals("CoreNotifyingEnabled", StringComparison.OrdinalIgnoreCase)).Value))
            {
                foreach (var recipient in appSettings
                    .First(x => x.Key.Equals("CoreNotifyingList", StringComparison.OrdinalIgnoreCase)).Value.Split(','))
                {
                    msg.Bcc.Add(recipient);
                }
            }

            SmtpClient client = new SmtpClient
            {
                UseDefaultCredentials = false,
                Host = appSettings.First(x => x.Key.Equals("CoreSmtpHost", StringComparison.OrdinalIgnoreCase)).Value,
                Port = Convert.ToInt32(appSettings.First(x => x.Key.Equals("CoreSmtpPort", StringComparison.OrdinalIgnoreCase)).Value),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials =
                    new NetworkCredential(
                        appSettings.First(x => x.Key.Equals("CoreEmailFrom", StringComparison.OrdinalIgnoreCase)).Value,
                        appSettings.First(x => x.Key.Equals("CoreEmailPassword", StringComparison.OrdinalIgnoreCase)).Value),
                Timeout = 20000
            };

            try
            {
                client.Send(msg);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                msg.Dispose();
            }
            return true;
        }
    }
}
