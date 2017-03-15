﻿using System.Collections.Generic;

namespace PunchClock.Model.Mapper
{
    public partial class Map
    {
        public void ViewToDomain(View.Model.User view, Domain.Model.User domain)
        {
            //Todo: Need to Implement
        }
        public void DomainToView(View.Model.User view, Domain.Model.User domain)
        {
            //Todo: Need to Implement
        }
        public void ViewToDomain(List<View.Model.User> views, List<Domain.Model.User> domains)
        {
            foreach (var view in views)
            {
                var domain = new Domain.Model.User();
                ViewToDomain(view, domain);
                domains.Add(domain);
            }
        }
        public void DomainToView(List<View.Model.User> views, List<Domain.Model.User> domains)
        {
            foreach (var domain in domains)
            {
                var view = new View.Model.User();
                DomainToView(view, domain);
                views.Add(view);
            }
        }
    }
}
