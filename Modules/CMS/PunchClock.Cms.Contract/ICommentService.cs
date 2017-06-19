using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PunchClock.Cms.Model;

namespace PunchClock.Cms.Contract
{
    public interface ICommentService
    {
        ArticleComments Add(ArticleComments comment);
        ArticleComments Update(ArticleComments comments);
        CmsResponse Delete(int id);
    }
}
