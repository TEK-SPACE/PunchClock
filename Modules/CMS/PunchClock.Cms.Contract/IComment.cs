using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PunchClock.Cms.Model;

namespace PunchClock.Cms.Contract
{
    public interface IComment
    {
        int Add(ArticleComments comment);
        int Update(ArticleComments comments);
         bool Delete(int id);
    }
}
