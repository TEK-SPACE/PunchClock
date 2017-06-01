using System.Collections.Generic;

namespace PunchClock.Model.Mapper
{
    public partial class Map
    {
        public void ViewToDomain(View.Model.CompanyView view, Domain.Model.Company domain)
        {
            //Todo: Need to Implement
            domain.Id = view.CompanyId;
            domain.GlobalId = view.GlobalId;
            domain.Name = view.Name;
            domain.RegisterCode = view.RegisterCode;
            domain.IsActive = view.IsActive;
            domain.LogoUrl = view.LogoUrl;
            domain.LogoBinary = view.LogoBinary;
            domain.Summary = view.Summary;
            domain.IsDeleted = view.IsDeleted;
            domain.DeltaPunchTime = view.DeltaPunchTime;
        }
        public void DomainToView(View.Model.CompanyView view, Domain.Model.Company domain)
        {
            //Todo: Need to Implement
            view.CompanyId = domain.Id;
            view.DeltaPunchTime = domain.DeltaPunchTime;
            view.GlobalId = domain.GlobalId;
            view.IsActive = domain.IsActive;
            view.IsDeleted = domain.IsDeleted;
            view.LogoBinary = domain.LogoBinary;
            view.LogoUrl = domain.LogoUrl;
            view.Name = domain.Name;
            view.RegisterCode = domain.RegisterCode;
            view.Summary = domain.Summary;
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
