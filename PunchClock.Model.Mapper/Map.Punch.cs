using System;
using System.Collections.Generic;
using PunchClock.Domain.Model;
using PunchClock.View.Model;

namespace PunchClock.Model.Mapper
{
    public partial class Map
    {
        public void ViewToDomain(View.Model.PunchView view, Domain.Model.Punch domain)
        {
            domain.Id = view.UserId;
            domain.ManagerAccepted = view.IsManagerAccepted;
            domain.PunchDate = view.PunchDate;
            domain.PunchIn = view.PunchIn;
            domain.PunchOut = view.PunchOut;
            domain.RequestForApproval = view.RequestForApproval;
            domain.UserId = view.UserId;
            domain.IpAddress = view.IpAddress;
            domain.MacAddress = view.MacAddress;
            domain.UserGuid = view.UserGuid;
            //Todo: Need to Implement
        }
        public void DomainToView(View.Model.PunchView view, Domain.Model.Punch domain)
        {
            //Todo: Need to Implement
            view.PunchId = domain.Id;
            view.IsManagerAccepted = domain.ManagerAccepted;
            view.PunchDate = domain.PunchDate;
            view.PunchIn = domain.PunchIn;
            view.PunchOut = domain.PunchOut;
            view.RequestForApproval = domain.RequestForApproval;
            view.UserId = domain.UserId;
            view.IpAddress = domain.IpAddress;
            view.MacAddress = domain.MacAddress;
        }
        public void ViewToDomain(List<View.Model.PunchView> views, List<Domain.Model.Punch> domains)
        {
            foreach (var view in views)
            {
                var domain = new Domain.Model.Punch();
                ViewToDomain(view, domain);
                domains.Add(domain);
            }
        }
        public void DomainToView(List<View.Model.PunchView> views, List<Domain.Model.Punch> domains)
        {
            foreach (var domain in domains)
            {
                var view = new View.Model.PunchView();
                DomainToView(view, domain);
                views.Add(view);
            }
        }
    }
}
