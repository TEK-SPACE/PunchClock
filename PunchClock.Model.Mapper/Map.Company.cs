using System.Collections.Generic;

namespace PunchClock.Model.Mapper
{
    public partial class Map
    {
        public void ViewToDomain(View.Model.CompanyView view, Domain.Model.Company domain)
        {
            //Todo: Need to Implement
        }
        public void DomainToView(View.Model.CompanyView view, Domain.Model.Company domain)
        {
            //Todo: Need to Implement
        }
        public void ViewToDomain(List<View.Model.CompanyView> views, List<Domain.Model.Company> domains)
        {
            foreach (var view in views)
            {
                var domain = new Domain.Model.Company();
                ViewToDomain(view, domain);
                domains.Add(domain);
            }
        }
        public void DomainToView(List<View.Model.CompanyView> views, List<Domain.Model.Company> domains)
        {
            foreach (var domain in domains)
            {
                var view = new View.Model.CompanyView();
                DomainToView(view, domain);
                views.Add(view);
            }
        }
    }
}
