using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PunchClock.Cms.Model;

namespace PunchClock.Cms.Contract
{
    public interface ITags
    {
        int Add(ArticleTag articleTag);
        int Update(ArticleTag articleTag);
        bool Delete(int id);
    }
}
