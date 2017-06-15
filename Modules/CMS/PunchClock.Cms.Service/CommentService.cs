using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PunchClock.Cms.Contract;
using PunchClock.Cms.Model;
using PunchClock.Core.DataAccess;

namespace PunchClock.Cms.Service
{
   public class CommentService:IComment
    {

        public int Add(ArticleComments comment)
        {
            using (var context = new PunchClockDbContext())
            {
                comment.CreatedDate = DateTime.Now;
                comment.IsDeleted = false;
                context.ArticleComments.Add(comment);
                context.SaveChanges();
            }
            return comment.Id;
        }

        public int Update(ArticleComments comments)
        {
            using (var context = new PunchClockDbContext())
            {

                comments.ModifiedDate = DateTime.Now;
                comments.IsDeleted = false;
                context.SaveChanges();
            }
            return comments.Id;
        }

      public bool Delete(int id)
        {
            using (var context = new PunchClockDbContext())
            {
                var comment = context.ArticleComments.FirstOrDefault(x => x.Id == id);
                if (comment == null) return false;
                comment.ModifiedDate = DateTime.Now;
                comment.IsDeleted = true;
                context.SaveChanges();
                return true;
            }
        }
    }
}
