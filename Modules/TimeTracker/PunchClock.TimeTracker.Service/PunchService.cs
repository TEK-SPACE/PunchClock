using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PunchClock.Core.DataAccess;
using PunchClock.Domain.Model;
using PunchClock.TimeTracker.Contract;
using PunchClock.TimeTracker.Model;
using UserType = PunchClock.Domain.Model.Enum.UserType;

namespace PunchClock.TimeTracker.Service
{
    public class PunchService : IPunch
    {

        public Punch OpenLogByUser(int id)
        {
            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                return context.Punches.FirstOrDefault(x => x.UserId == id && x.PunchOut == null);
            }
        }

        public List<Punch> OpenLogsByUser(int id)
        {
            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                var user = context.Users.FirstOrDefault(x => x.Uid == id);
                if (user == null) return null;
                return context.Punches.Where(x => x.UserId == id && x.PunchOut == null).ToList();
            }
        }

        public string PunchIn(int userId, TimeSpan punchTime, string ipAddress, string macAdress)
        {
            string message = "Successfully punched in";

            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                var user = context.Users.FirstOrDefault(x => x.Uid == userId);
                if (user == null) return null;

                Punch punch = new Punch
                {
                    PunchDate = DateTime.UtcNow,
                    UserId = userId,
                    PunchIn = punchTime != TimeSpan.MinValue ? punchTime : DateTime.Now.ToUniversalTime().TimeOfDay,
                    IpAddress = ipAddress,
                    MacAddress = macAdress,
                    UserGuid = user.Id
                };
                ExplicitPunchTimeCheck(punchTime, punch);
                IpAddressMatchCheck(ipAddress, user, punch);
                context.Punches.Add(punch);
                context.SaveChanges();
            }

            message = string.Format(
                "<div class=\"{0}\">  <button type=\"button\" class=\"close punchCompleteMessage\" onclick=\"punchCompleteMessage();\" data-dismiss=\"alert\">×</button>" +
                message + "</div>",
                message == "Successfully punched in"
                    ? "alert alert-dismissable alert-success"
                    : "alert alert-dismissable alert-danger");
            return message;
        }

        public string PunchOut(int userId, int punchId, TimeSpan punchTime, string ipAddress, string macAdress)
        {
            string message = "Successfully punched Out";

            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                var user = context.Users.FirstOrDefault(x => x.Uid == userId);
                if (user == null) return null;

                var punch = context.Punches.FirstOrDefault(x => x.Id == punchId);
                if (punch == null) return null;

                if (punch.UserId != userId)
                    return "Invalid operation";

                punch.PunchOut = punchTime != TimeSpan.MinValue ? punchTime : DateTime.Now.ToUniversalTime().TimeOfDay;

                ExplicitPunchTimeCheck(punchTime, punch);

                if (DateTime.UtcNow.Date.Subtract(punch.PunchDate).Days != 0)
                {
                    if (string.IsNullOrEmpty(punch.Comments))
                        punch.Comments = string.Empty;
                    message = punch.Comments +=
                        " Due to discrepancy in punch timings, manager Approval is requested for this punch out time";
                    punch.ApprovalRequired = true;
                }
                IpAddressMatchCheck(ipAddress, user, punch);
                punch.IpAddress = ipAddress;
                punch.MacAddress = macAdress;

                context.SaveChanges();
            }

            message = string.Format(
                "<div class=\"{0}\">  <button type=\"button\" class=\"close punchCompleteMessage\" data-dismiss=\"alert\">×</button>" +
                message + "</div>",
                message == "Successfully punched Out"
                    ? "alert alert-dismissable alert-success"
                    : "alert alert-dismissable alert-danger");
            return message;
        }

        public List<Punch> Search(int opUserId, int userId, DateTime stDate, DateTime enDate)
        {
            PunchClockDbContext context = new PunchClockDbContext();
            {
                if (opUserId == userId)
                    return context.Punches.Where(x => x.UserId == userId && x.PunchOut != null).ToList();

                bool isUserAuthorizedTo = IsUserAuthorizedTo(opUserId, context);

                return isUserAuthorizedTo
                    ? context.Punches.Where(x => x.UserId == userId && x.PunchOut == null).ToList()
                    : null;
            }
        }



        public bool Approve(Punch punch, int opUserId)
        {
            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                var p = context.Punches.FirstOrDefault(x => x.Id == punch.Id);
                if (p == null) return false;

                bool isUserAuthorizedTo = IsUserAuthorizedTo(opUserId, context);

                if (isUserAuthorizedTo)
                {
                    p.PunchIn = punch.PunchIn;
                    p.PunchOut = punch.PunchOut;
                    p.Approved = punch.Approved;
                    p.PunchDate = punch.PunchDate;
                    p.Comments = punch.Comments;
                }
                context.SaveChanges();
                return true;
            }
        }

        public void Add(Punch punch)
        {
            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                context.Punches.Add(punch);
                context.SaveChanges();
            }
        }

        public List<Punch> All()
        {
            PunchClockDbContext context = new PunchClockDbContext();
            return context.Punches.Where(x => x.PunchOut != null).Include(x=>x.User).ToList();
        }

        public void Update(Punch punch)
        {
            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                var updateEntity = context.Punches.FirstOrDefault(x => x.Id == punch.Id);
                if (updateEntity != null)
                {
                    updateEntity.PunchDate = punch.PunchDate;
                    updateEntity.PunchIn = punch.PunchIn;
                    updateEntity.PunchOut = punch.PunchOut;
                    context.SaveChanges();
                }
            }
        }

        public void Delete(Punch punch)
        {
            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                var updateEntity = context.Punches.FirstOrDefault(x => x.Id == punch.Id);
                if (updateEntity != null)
                {
                    context.Punches.Remove(updateEntity);
                    context.SaveChanges();
                }
            }
        }

        public void Approve(Punch punch)
        {
            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                var updateEntity = context.Punches.FirstOrDefault(x => x.Id == punch.Id);
                if (updateEntity != null)
                {
                    updateEntity.Approved = true;
                    context.SaveChanges();
                }
            }
        }

        public void Approve(int id)
        {
            using (PunchClockDbContext context = new PunchClockDbContext())
            {
                var updateEntity = context.Punches.FirstOrDefault(x => x.Id == id);
                if (updateEntity != null)
                {
                    updateEntity.Approved = true;
                    context.SaveChanges();
                }
            }
        }

        #region Private Methods

        private static bool IsUserAuthorizedTo(int opUserId, PunchClockDbContext context)
        {
            var user = context.Users.FirstOrDefault(x => x.Uid == opUserId);
            return user?.UserTypeId != (int)UserType.Employee;
        }

        private static void ExplicitPunchTimeCheck(TimeSpan punchTime, Punch punch)
        {
            if (punchTime != TimeSpan.MinValue)
            {
                if (string.IsNullOrEmpty(punch.Comments))
                    punch.Comments = string.Empty;
                punch.Comments += " user requested explicit punch time";
                punch.ApprovalRequired = true;
            }
        }
        private static void IpAddressMatchCheck(string ipAddress, User user, Punch punch)
        {
            if (user.UserRegisteredIp != ipAddress)
            {
                if (string.IsNullOrEmpty(punch.Comments))
                    punch.Comments = string.Empty;
                punch.Comments += " Punches from new IP address";
                punch.ApprovalRequired = true;
            }
        }

        #endregion
    }
}