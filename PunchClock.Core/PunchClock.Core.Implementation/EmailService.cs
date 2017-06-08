using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using PunchClock.Core.Contracts;
using PunchClock.Core.Models.Common;
using PunchClock.Domain.Model;
using PunchClock.View.Model;

namespace PunchClock.Core.Implementation
{
    public class EmailService : IEmailRepository
    {
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
            MailMessage msg = new MailMessage
            {
                //ConfigurationManager.AppSettings["EmailFromTitle"]
                From = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"], "PunchClock"),
                Subject = msgSubject,
                Body = msgBody,
                IsBodyHtml = true
            };
            foreach (var recipient in recipients)
            {
                msg.To.Add(recipient);
            }
            msg.Bcc.Add(ConfigurationManager.AppSettings["CompanyEMail"]);

            SmtpClient client = new SmtpClient
            {
                UseDefaultCredentials = false,
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials =
                    new NetworkCredential(ConfigurationManager.AppSettings["EmailFrom"],
                        ConfigurationManager.AppSettings["SalesEmailPassword"]),
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
