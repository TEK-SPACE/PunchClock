using System.Collections.Generic;
using PunchClock.Core.Models.Common;
using ArticleType = PunchClock.Cms.Model.ArticleType;

namespace PunchClock.Cms.Contract
{
    public interface ITypeService
    {
        ArticleType Add(Model.ArticleType articleType);
        ArticleType Update(Model.ArticleType articleType);
        AjaxResponse Delete(int id);
        ArticleType GetOneArticleType(int id);
        List<ArticleType> GetAllArticleTypes();
        List<ArticleType> GetArticleTypesBbyCompanyId(int companyId);
    }
}