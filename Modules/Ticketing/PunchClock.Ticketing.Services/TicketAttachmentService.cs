using System;
using System.Linq;
using PunchClock.Core.DataAccess;
using PunchClock.Ticketing.Contracts;
using PunchClock.Ticketing.Model;
using PunchClock.Domain.Model;

namespace PunchClock.Ticketing.Services
{
    public class TicketAttachmentService : ITicketAttachment
    {
        public TicketAttachment Add(TicketAttachment attachment)
        {
            using (var context = new PunchClockDbContext())
            {
                attachment.CreatedDateUtc = DateTime.UtcNow;
                context.TicketAttachments.Add(attachment);
                context.SaveChanges();
            }
            return attachment;
        }
        public AjaxResponse Delete(int id)
        {
            var response = new AjaxResponse
            {
                ResponseId =id,
                ResponseText = "Record is not deleted",
                Success = false
            };
            using (var context = new PunchClockDbContext())
            {
                var ticketAttachment = context.TicketAttachments.FirstOrDefault(x => x.Id == id);
                if (ticketAttachment == null) return response;
                response.Success = true;
                response.ResponseText = "Data Deleted Successfully";
                return response;
            }
        }
        public TicketAttachment Update(TicketAttachment atachment)
        {
            using (var context = new PunchClockDbContext())
            {
                var existingAttach = context.TicketAttachments.FirstOrDefault(x => x.Id == atachment.Id);
                if (existingAttach == null) return atachment;
                existingAttach.Id = atachment.Id;
                existingAttach.Data = atachment.Data;
                existingAttach.ModifiedDateUtc = DateTime.UtcNow;
                existingAttach.CreatedDateUtc = DateTime.UtcNow;
                existingAttach.IsDeleted = false;
                context.SaveChanges();
            }
            return atachment;
        }
    }
}
