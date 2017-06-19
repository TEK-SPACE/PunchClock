using System;
using System.Linq;
using PunchClock.Cms.Contract;
using PunchClock.Cms.Model;
using PunchClock.Core.DataAccess;

namespace PunchClock.Cms.Service
{
   public class CommentService:ICommentService
    {

        public ArticleComments Add(ArticleComments comment)
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

        public ArticleComments Update(ArticleComments comments)
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

      public CmsResponse Delete(int id)
        {
            var response = new CmsResponse
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
