using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PunchClock.Model.Mapper
{
    public partial class Map
    {
        public void ViewToDomain(View.Model.Punch view, Domain.Model.Punch domain)
        {
            //Todo: Need to Implement
        }
        public void DomainToView(View.Model.Punch view, Domain.Model.Punch domain)
        {
            //Todo: Need to Implement
        }
        public void ViewToDomain(List<View.Model.Punch> views, List<Domain.Model.Punch> domains)
        {
            foreach (var view in views)
            {
                var domain = new Domain.Model.Punch();
                ViewToDomain(view, domain);
                domains.Add(domain);
            }
        }
        public void DomainToView(List<View.Model.Punch> views, List<Domain.Model.Punch> domains)
        {
            foreach (var domain in domains)
            {
                var view = new View.Model.Punch();
                DomainToView(view, domain);
                views.Add(view);
            }
        }
    }
}
