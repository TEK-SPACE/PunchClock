using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PunchClock.Cms.Model;

namespace PunchClock.Cms.Contract
{
   public interface IType
   {
       int Add(ArticleType articleType);
       int Update(ArticleType articleType);
       bool Delete(int id);
   }
}
