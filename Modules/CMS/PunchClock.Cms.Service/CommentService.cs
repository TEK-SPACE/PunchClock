using System;
using System.Linq;
using PunchClock.Cms.Contract;
using PunchClock.Cms.Model;
using PunchClock.Core.DataAccess;
using PunchClock.Core.Models.Common;

namespace PunchClock.Cms.Service
{
   public class CommentService: ICommentService
    {

        public Comment Add(Comment comment)
        {
            using (var context = new PunchClockDbContext())
            {
                comment.CreatedDate = DateTime.Now;
                comment.IsDeleted = false;
                context.ArticleComments.Add(comment);
                context.SaveChanges();
            }
            return comment;
        }

        public Comment Update(Comment comments)
        {
            using (var context = new PunchClockDbContext())
            {
                var existingComment = context.ArticleComments.FirstOrDefault(x => x.Id == comments.Id);
                if (existingComment == null) return comments;
                existingComment.ModifiedDate = DateTime.Now;
                existingComment.Description = comments.Description;
                existingComment.ArticleId = comments.ArticleId;
                existingComment.IsDeleted = false;
                context.SaveChanges();
            }
            return comments;
        }

      public AjaxResponse Delete(int id)
        {
            var response = new AjaxResponse
            {
                ResponseId = id,
                ResponseText = "Record is not deleted",
                Success = false
            };
            using (var context = new PunchClockDbContext())
            {
                var comment = context.ArticleComments.FirstOrDefault(x => x.Id == id);
                if (comment == null) return response;
                comment.ModifiedDate = DateTime.Now;
                comment.IsDeleted = true;
                context.SaveChanges();
                response.ResponseText = "Comment is Deleted.";
                response.Success = true;
                return response;
            }
        }
    }
}
