using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PunchClock.Language.Model
{
    public class ArticleTagResource : BaseResource
    {
        [Index("UniqueResourceArticleTypeCulture", 1)]
        public int TagMasterId { get; set; }
        [Index("UniqueResourceArticleTypeCulture", 2)]
        public Culture Culture { get; set; }
    }
}
