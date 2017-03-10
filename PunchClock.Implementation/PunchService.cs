using PunchClock.DAL;
using PunchClock.Objects.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Omu.ValueInjecter;
using System.Data.Entity;
using PunchClock.Model;

namespace PunchClock.Implementation
{
    public class PunchService
    {
        public PunchObjectLibrary OpUserOpenLog(int opUserId)
        {
            PunchObjectLibrary punchObjectLibrary = new PunchObjectLibrary();

            using (var unitOfWork = new UnitOfWork())
            {
                var punch = unitOfWork.PunchRepository.Get(filter: x => x.UserId == opUserId && x.PunchOut == null).FirstOrDefault();
                if (punch != null)
                    punchObjectLibrary.InjectFrom(punch);

            }
            return punchObjectLibrary;
        }

        public List<PunchObjectLibrary> GetOpenLogs(int opUserId)
        {
            List<PunchObjectLibrary> punchList = new List<PunchObjectLibrary>();
            using (var unitOfWork = new UnitOfWork())
            {
                var user = unitOfWork.UserRepository.Get(filter: x => x.UserId == opUserId).FirstOrDefault();
                if (user != null)
                {
                    var punch = (from p in unitOfWork.PunchRepository.Get()
                                  join u in unitOfWork.UserRepository.Get() on p.UserId equals u.UserId
                                  where u.CompanyId == user.CompanyId
                                  select p).ToList();
                    punch.ForEach(p =>
                        punchList.Add(new PunchObjectLibrary
                        {
                            IsManagerAccepted = p.IsManagerAccepted,
                            PunchDate = p.PunchDate,
                            PunchId = p.PunchId,
                            PunchIn = p.PunchIn,
                            PunchOut = p.PunchOut,
                            UserId = p.UserId,
                            EmployeeName = user.FirstName + " " + user.LastName,
                            Hours = DbFunctions.DiffSeconds(p.PunchIn, p.PunchOut),
                            Comments = p.Comments
                        }));
                }
            }
            return punchList;
        }

        public string PunchIn(int userId, TimeSpan punchTime, string ipAddress)
        {
            string message = "Successfully punched in";
            using (var unitOfWork = new UnitOfWork())
            {
                var user = unitOfWork.UserRepository.Get(x => x.UserId == userId).FirstOrDefault();
                Punch punch = new Punch
                {
                    UserId = userId,
                    PunchDate = DateTime.UtcNow,
                    PunchIn = punchTime != TimeSpan.MinValue ? punchTime : DateTime.Now.ToUniversalTime().TimeOfDay
                };
                if (punchTime != TimeSpan.MinValue)
                {
                    if (string.IsNullOrEmpty(punch.Comments))
                        punch.Comments = string.Empty;
                    punch.Comments += " user requested explicit punch time";
                    punch.RequestForApproval = true;
                }
                if (user != null && user.UserRegistered_IP != ipAddress)
                {
                    if (string.IsNullOrEmpty(punch.Comments))
                        punch.Comments = string.Empty;
                    punch.Comments += "Punch from new IP address";
                    punch.RequestForApproval = true;
                }
                unitOfWork.PunchRepository.Insert(punch);
                try
                {
                    unitOfWork.Save();
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }
            }
            message = string.Format("<div class=\"{0}\">  <button type=\"button\" class=\"close punchCompleteMessage\" data-dismiss=\"alert\">×</button>" + message + "</div>",
              message == "Successfully punched in"
              ? "alert alert-dismissable alert-success"
              : "alert alert-dismissable alert-danger");
            return message;
        }

        public string PunchOut(int userId, int punchId, TimeSpan punchTime, string ipAddress)
        {
            string message = "Successfully punched Out";
            using (var unitOfWork = new UnitOfWork())
            {
                var user = unitOfWork.UserRepository.Get(x => x.UserId == userId).FirstOrDefault();
                var punch = unitOfWork.PunchRepository.Get(x => x.PunchId == punchId).SingleOrDefault();

                if (punch != null)
                {
                    punch.PunchOut = punchTime != TimeSpan.MinValue ? punchTime : DateTime.Now.ToUniversalTime().TimeOfDay;
                    if (punch.UserId != userId)
                        return "Invalid operation";
                    if (punchTime != TimeSpan.MinValue)
                    {
                        if (string.IsNullOrEmpty(punch.Comments))
                            punch.Comments = string.Empty;
                        punch.Comments += " User requested explicit punch time";
                        punch.RequestForApproval = true;
                    }
                    if (DateTime.UtcNow.Date.Subtract(punch.PunchDate).Days != 0)
                    {
                        if (string.IsNullOrEmpty(punch.Comments))
                            punch.Comments = string.Empty;
                        message = punch.Comments += " Due to discrepancy in punch timings, manager Approval is requested for this punch out time";
                        punch.RequestForApproval = true;
                    }
                    if (user != null && user.UserRegistered_IP != ipAddress)
                    {
                        if (string.IsNullOrEmpty(punch.Comments))
                            punch.Comments = string.Empty;
                        punch.Comments += " Punch from new IP address";
                        punch.RequestForApproval = true;
                    }
                }
                try
                {
                    unitOfWork.Save();
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }
            }
            message = string.Format("<div class=\"{0}\">  <button type=\"button\" class=\"close punchCompleteMessage\" data-dismiss=\"alert\">×</button>" + message + "</div>",
                message == "Successfully punched Out"
                ? "alert alert-dismissable alert-success"
                : "alert alert-dismissable alert-danger");
            return message;
        }

        public List<PunchObjectLibrary> Search(int operatingUserId,int userId, DateTime stDate, DateTime enDate)
        {
            List<PunchObjectLibrary> punchList;
            using (var unitOfWork = new UnitOfWork())
            {
                punchList = (from p in unitOfWork.PunchRepository.Get()
                             where p.UserId == userId
                    let pPunchOut = p.PunchOut
                    // && stDate.Subtract(p.PunchDate).Days >= 0
                            // && enDate.Subtract(p.PunchDate).Days <= 0
                             //&& p.PunchOut != null
                    where pPunchOut != null
                    select new PunchObjectLibrary
                             {
                                 IsManagerAccepted = p.IsManagerAccepted,
                                 RequestForApproval = p.RequestForApproval,
                                 PunchDate = p.PunchDate,
                                 PunchId = p.PunchId,
                                 PunchIn = p.PunchIn,
                                 PunchOut = pPunchOut,
                                 UserId = p.UserId,
                                 ApprovedHours = p.RequestForApproval
                                 ? (p.IsManagerAccepted ? pPunchOut.Value.Subtract(p.PunchIn).Seconds : 0)
                                 : pPunchOut.Value.Subtract(p.PunchIn).Seconds,
                                 Hours = pPunchOut.Value.Subtract(p.PunchIn).Seconds,
                                 Comments = p.Comments
                             }).ToList();
            }
            return punchList;
        }

        public bool Approve(PunchObjectLibrary punchObj, int opUserId)
        {
            bool retValue;
            using (var unitOfWork = new UnitOfWork())
            {
                var punch = unitOfWork.PunchRepository.Get(x => x.PunchId == punchObj.PunchId).FirstOrDefault();
                if (punch != null)
                {
                    punch.PunchIn = punchObj.PunchIn;
                    punch.PunchOut = punchObj.PunchOut;
                    punch.IsManagerAccepted = punchObj.IsManagerAccepted;
                    punch.PunchDate = punchObj.PunchDate;
                    punch.Comments = punchObj.Comments;
                }
                try
                {
                    unitOfWork.Save();
                    retValue = true;
                }
                catch (Exception)
                {
                    retValue = false;
                }
            }
            return retValue;
        }
    }
}
