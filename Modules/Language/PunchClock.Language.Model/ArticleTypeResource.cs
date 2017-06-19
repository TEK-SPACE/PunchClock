using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
