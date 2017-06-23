using System.ComponentModel.DataAnnotations.Schema;
using PunchClock.Language.Model;

namespace PunchClock.Cms.Model
{
    public class ArticleTypeResource : BaseResource
    {
        [Index("UniqueResourceArticleTypeCulture", 1)]
        public int TypeMasterId { get; set; }
        [Index("UniqueResourceArticleTypeCulture", 2)]
        public Culture Culture { get; set; }

        [ForeignKey("TypeMasterId")]
        public ArticleType ArticleType { get; set; }
    }
}
