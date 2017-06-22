using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PunchClock.Language.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace PunchClock.Cms.Model
{
    public class ArticleCategoryResource : BaseResource
    {
        [Index("UniqueResourceArticleTypeCulture", 1)]
        public int CategoryMasterId { get; set; }
        [Index("UniqueResourceArticleTypeCulture", 2)]
        public Culture Culture { get; set; }

        [ForeignKey("CategoryMasterId")]
        public ArticleCategory ArticleCategory { get; set; }
    }
}
