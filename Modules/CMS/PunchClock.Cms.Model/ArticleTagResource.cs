using System.ComponentModel.DataAnnotations.Schema;
using PunchClock.Language.Model;

namespace PunchClock.Cms.Model
{
    public class ArticleTagResource : BaseResource
    {
        [Index("UniqueResourceArticleTypeCulture", 1)]
        public int TagMasterId { get; set; }
        [Index("UniqueResourceArticleTypeCulture", 2)]
        public Culture Culture { get; set; }
        [ForeignKey("TagMasterId")]
        public ArticleTag ArticleTag { get; set; }
    }
}
