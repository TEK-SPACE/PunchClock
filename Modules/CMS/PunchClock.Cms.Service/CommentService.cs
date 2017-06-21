using System;
using System.Collections.Generic;
using System.Linq;
using PunchClock.Cms.Contract;
using PunchClock.Cms.Model;
using PunchClock.Core.DataAccess;
using PunchClock.Core.Models.Common;

namespace PunchClock.Cms.Service
{
   public class CommentService: ICommentService
    {

        public ArticleComment Add(ArticleComment comment)
        {
            using (var context = new PunchClockDbContext())
            {
                comment.IsDeleted = false;
                context.ArticleComments.Add(comment);
                context.SaveChanges();
            }
            return comment;
        }

        public ArticleComment Update(ArticleComment comments)
        {
            using (var context = new PunchClockDbContext())
            {
                var existingComment = context.ArticleComments.FirstOrDefault(x => x.Id == comments.Id);
                if (existingComment == null) return comments;
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
                comment.IsDeleted = true;
                context.SaveChanges();
                response.ResponseText = "Comment is Deleted.";
                response.Success = true;
                return response;
            }
        }

        public ArticleComment GetOneArticleComment(int id)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.ArticleComments.FirstOrDefault(x => x.Id == id);
            }
        }

        public List<ArticleComment> GetAllCommentsByArticleId(int articleId)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.ArticleComments.Where(x => x.IsDeleted == false && x.ArticleId == articleId).ToList();
            }
        }

        public List<ArticleComment> GetAllCOmmentsByCompanyId(int companyId)
        {
            using (var context = new PunchClockDbContext())
            {
                return context.ArticleComments.Where(x => x.IsDeleted == false && x.CompanyId == companyId).ToList();
            }
        }
    }
}
