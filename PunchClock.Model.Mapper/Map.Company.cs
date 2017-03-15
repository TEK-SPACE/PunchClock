using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PunchClock.Domain.Model;
using PunchClock.View.Model;

namespace PunchClock.Model.Mapper
{
    public partial class Map
    {
        public void ViewToDomain(View.Model.Company view, Domain.Model.Company domain)
        {
            //Todo: Need to Implement
        }
        public void DomainToView(View.Model.Company view, Domain.Model.Company domain)
        {
            //Todo: Need to Implement
        }
        public void ViewToDomain(List<View.Model.Company> views, List<Domain.Model.Company> domains)
        {
            foreach (var view in views)
            {
                var domain = new Domain.Model.Company();
                ViewToDomain(view, domain);
                domains.Add(domain);
            }
        }
        public void DomainToView(List<View.Model.Company> views, List<Domain.Model.Company> domains)
        {
            foreach (var domain in domains)
            {
                var view = new View.Model.Company();
                DomainToView(view, domain);
                views.Add(view);
            }
        }
    }
}
