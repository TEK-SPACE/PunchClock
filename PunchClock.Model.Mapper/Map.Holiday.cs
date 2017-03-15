using System.Collections.Generic;

namespace PunchClock.Model.Mapper
{
    public partial class Map
    {
        public void ViewToDomain(View.Model.HolidayView view, Domain.Model.Holiday domain)
        {
            //Todo: Need to Implement
        }
        public void DomainToView(View.Model.HolidayView view, Domain.Model.Holiday domain)
        {
            //Todo: Need to Implement
        }
        public void ViewToDomain(List<View.Model.HolidayView> views, List<Domain.Model.Holiday> domains)
        {
            foreach (var view in views)
            {
                var domain = new Domain.Model.Holiday();
                ViewToDomain(view, domain);
                domains.Add(domain);
            }
        }
        public void DomainToView(List<View.Model.HolidayView> views, List<Domain.Model.Holiday> domains)
        {
            foreach (var domain in domains)
            {
                var view = new View.Model.HolidayView();
                DomainToView(view, domain);
                views.Add(view);
            }
        }
    }
}
