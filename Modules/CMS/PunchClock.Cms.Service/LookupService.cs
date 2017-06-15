using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PunchClock.Cms.Contract;
using PunchClock.Cms.Model;
using ArticleType = PunchClock.Cms.Model.ArticleType;

namespace PunchClock.Cms.Service
{
   public class LookupService:ILookup
    {
        public Article GetOneArticle(int articleId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Article> GetManyArticles()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ArticleComments> GetCommentsByArticleId(int artcileId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ArticleTag> GeTags()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ArticleType> GetTypes()
        {
            throw new NotImplementedException();
        }
    }
}
