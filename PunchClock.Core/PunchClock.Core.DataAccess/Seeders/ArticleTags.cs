using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PunchClock.Cms.Model;
using PunchClock.Language.Model;

namespace PunchClock.Core.DataAccess.Seeders
{
    public partial class PunchDbInitializer
    {
        private void SeedArticleTags(PunchClockDbContext context)
        {
            var articleTags = new List<ArticleTag>
            {
                new ArticleTag
                {
                   Name = "Home",
                    Description = "",
                     CreatedDateUtc = DateTime.UtcNow,
                    CreatedBy = null,
                    ModifiedById = null,
                    ModifiedDateUtc = DateTime.UtcNow,
                    CompanyId = 1
                },
                new ArticleTag
                {
                    Name = "Social",
                    Description = "",
                  CreatedDateUtc = DateTime.UtcNow,
                    CreatedBy = null,
                    ModifiedById = null,
                    ModifiedDateUtc = DateTime.UtcNow,
                       CompanyId = 1
                }
                ,
                new ArticleTag
                {
                    Name = "Fashion",
                    Description = "",
                     CreatedDateUtc = DateTime.UtcNow,
                    CreatedBy = null,
                    ModifiedById = null,
                    ModifiedDateUtc = DateTime.UtcNow,
                       CompanyId = 1
                }
                 ,
                new ArticleTag
                {
                    Name = "Technology",
                    Description = "",
                 CreatedDateUtc = DateTime.UtcNow,
                    CreatedBy = null,
                    ModifiedById = null,
                    ModifiedDateUtc = DateTime.UtcNow,
                       CompanyId = 1
                }
            };
            foreach (var tag in articleTags)
            {
                context.ArticleTags.AddOrUpdate(tag);
             
            }
        }


    }
}
