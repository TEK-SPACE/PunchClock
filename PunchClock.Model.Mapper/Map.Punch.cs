using System.Collections.Generic;

namespace PunchClock.Model.Mapper
{
    public partial class Map
    {
        public void ViewToDomain(View.Model.PunchView view, Domain.Model.Punch domain)
        {
            //Todo: Need to Implement
        }
        public void DomainToView(View.Model.PunchView view, Domain.Model.Punch domain)
        {
            //Todo: Need to Implement
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
