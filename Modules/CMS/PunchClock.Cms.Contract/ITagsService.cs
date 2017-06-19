using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PunchClock.Cms.Model;

namespace PunchClock.Cms.Contract
{
    public interface ITagsService
    {
        ArticleTag Add(ArticleTag articleTag);
        ArticleTag Update(ArticleTag articleTag);
        CmsResponse Delete(int id);
    }
}
