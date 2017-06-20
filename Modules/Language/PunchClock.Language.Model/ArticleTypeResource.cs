using System.ComponentModel.DataAnnotations.Schema;

namespace PunchClock.Language.Model
{
    public class ArticleTypeResource : BaseResource
    {
        [Index("UniqueResourceArticleTypeCulture", 1)]
        public int TypeMasterId { get; set; }
        [Index("UniqueResourceArticleTypeCulture", 2)]
        public Culture Culture { get; set; }
    }
}
