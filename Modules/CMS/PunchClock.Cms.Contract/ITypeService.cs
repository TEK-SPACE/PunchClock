using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PunchClock.Cms.Model;

namespace PunchClock.Cms.Contract
{
   public interface ITypeService
   {
        ArticleType Add(ArticleType articleType);
        ArticleType Update(ArticleType articleType);
        CmsResponse Delete(int id);
   }
}
