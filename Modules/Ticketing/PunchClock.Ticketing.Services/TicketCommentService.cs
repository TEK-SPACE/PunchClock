using System;
using System.Linq;
using PunchClock.Core.DataAccess;
using PunchClock.Ticketing.Contracts;
using PunchClock.Ticketing.Model;
using PunchClock.Domain.Model;

namespace PunchClock.Ticketing.Services
{
    class TicketCommentService : ITicketComment
    {
        public TicketComment Add(TicketComment comment)
        {
            using (var context = new PunchClockDbContext())
            {
                {
                    comment.CreatedDateUtc = DateTime.UtcNow;
                    context.TicketComments.Add(comment);
                    context.SaveChanges();
                }
                return comment;
            }
        }
        public AjaxResponse Delete(int Id)
        {
            var responce = new AjaxResponse
            {
                ResponseId = Id,
                ResponseText = "Record Is not Deleted",
                Success = false,
            };

            using (var context = new PunchClockDbContext())
            {
                var ticketComment = context.TicketComments.FirstOrDefault(x => x.Id == Id);
                if (ticketComment == null)
                    return responce;
                responce.Success = true;
                responce.ResponseText = "Record Deleted Successfully";
                return responce;
            }
        }

        public TicketComment Update(TicketComment comment)
        {
            using (var context = new PunchClockDbContext())
            {
                var oldcomment = context.TicketComments.FirstOrDefault(x => x.Id == comment.Id);
                if (oldcomment == null) return comment;
                comment.IsDeleted = false;
                comment.ModifiedById = "xxx";
                comment.CreatedDateUtc = DateTime.UtcNow;
                comment.ModifiedDateUtc = DateTime.UtcNow;
                context.SaveChanges();
            }
            return comment;

        }
    }
}
