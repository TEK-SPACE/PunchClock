using System;
using System.Collections.Generic;
using PunchClock.TimeTracker.Model;

namespace PunchClock.TimeTracker.Contract
{
    public interface IPunch
    {
        Punch OpenLogByUser(int id);
        List<Punch> OpenLogsByUser(int id);
        string PunchIn(int userId, TimeSpan punchTime, string ipAddress, string macAdress);
        string PunchOut(int userId, int punchId, TimeSpan punchTime, string ipAddress, string macAdress);
        List<Punch> Search(int opUserId, int userId, DateTime stDate, DateTime enDate);
        bool Approve(Punch punch, int opUserId);
        void Add(Punch punch);
        List<Punch> All();
        void Update(Punch punch);
        void Delete(Punch punch);
        void Approve(Punch punch);
    }
}