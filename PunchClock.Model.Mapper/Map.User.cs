using System.Collections.Generic;

namespace PunchClock.Model.Mapper
{
    public partial class Map
    {
        public void ViewToDomain(View.Model.UserView view, Domain.Model.User domain)
        {
            //Todo: Need to Implement
            domain.UserName = view.UserName;
            domain.CompanyId = view.CompanyId;
            domain.DateCreatedUtc = view.DateCreatedUtc;
            domain.Email = view.Email;
            domain.EmploymentTypeId = view.EmploymentTypeId;
            domain.FirstName = view.FirstName;
            domain.GlobalId = view.GlobalId;
            domain.IsActive = view.IsActive;
            domain.IsAdmin = view.IsAdmin;
            domain.IsDeleted = view.IsDeleted;
            domain.LastActiveMacAddress = view.LastActiveMacAddress;
            domain.LastActivityDateUtc = view.LastActivityDateUtc;
            domain.LastActivityIp = view.LastActivityIp;
            domain.LastName = view.LastName;
            domain.LastUpdatedUtc = view.LastUpdatedUtc;
            domain.MiddleName = view.MiddleName;
            domain.PasswordDisabled = view.PasswordDisabled;
            domain.PasswordHash = view.PasswordHash;
            domain.PasswordLastChanged = view.PasswordLastChanged;
            domain.PasswordSalt = view.PasswordSalt;
            domain.RegisteredMacAddress =view.RegisteredMacAddress;
            domain.RegisteredTimeZone = view.RegisteredTimeZone;
            domain.PhoneNumber = view.PhoneNumber;
            domain.Uid = view.UserId;
            domain.UserName = view.UserName;
            domain.UserRegisteredIp = view.UserRegisteredIp;
            domain.UserTypeId = view.UserTypeId;
            if(!string.IsNullOrWhiteSpace(view.Id))
                domain.Id = view.Id;
        }
        public void DomainToView(View.Model.UserView view, Domain.Model.User domain)
        {
            //Todo: Need to Implement
            view.UserName = domain.UserName;
            view.CompanyId = domain.CompanyId;
            view.DateCreatedUtc = domain.DateCreatedUtc;
            view.Email = domain.Email;
            view.EmploymentTypeId = domain.EmploymentTypeId;
            view.FirstName = domain.FirstName;
            view.GlobalId = domain.GlobalId;
            view.IsActive = domain.IsActive;
            view.IsAdmin = domain.IsAdmin;
            view.IsDeleted = domain.IsDeleted;
            view.LastActiveMacAddress = domain.LastActiveMacAddress;
            view.LastActivityDateUtc = domain.LastActivityDateUtc;
            view.LastActivityIp = domain.LastActivityIp;
            view.LastName = domain.LastName;
            view.MiddleName = domain.MiddleName;
            view.PasswordDisabled = domain.PasswordDisabled;
            view.PasswordHash = domain.PasswordHash;
            view.PasswordSalt = domain.PasswordSalt;
            view.RegisteredMacAddress = domain.RegisteredMacAddress;
            view.RegisteredTimeZone = domain.RegisteredTimeZone;
            view.PhoneNumber = domain.PhoneNumber;
            view.UserName = domain.UserName;
            view.UserRegisteredIp = domain.UserRegisteredIp;
            view.UserTypeId = domain.UserTypeId;
            view.UserId = domain.Uid;
            view.LastUpdatedUtc = domain.LastUpdatedUtc;
            view.PasswordLastChanged = domain.PasswordLastChanged;
            view.Id = domain.Id;
        }
        public void ViewToDomain(List<View.Model.UserView> views, List<Domain.Model.User> domains)
        {
            foreach (var view in views)
            {
                var domain = new Domain.Model.User();
                ViewToDomain(view, domain);
                domains.Add(domain);
            }
        }
        public void DomainToView(List<View.Model.UserView> views, List<Domain.Model.User> domains)
        {
            foreach (var domain in domains)
            {
                var view = new View.Model.UserView();
                DomainToView(view, domain);
                views.Add(view);
            }
        }
    }
}
