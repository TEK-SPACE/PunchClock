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
                    Id = 1,
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
                    Id = 2,
                    Name = "Social",
                    Description = "",
                    CreatedDateUtc = DateTime.UtcNow,
                    CreatedBy = null,
                    ModifiedById = null,
                    ModifiedDateUtc = DateTime.UtcNow,
                    CompanyId = context.Companies.First().Id
                }
                ,
                new ArticleTag
                {
                    Id = 3,
                    Name = "Fashion",
                    Description = "",
                    CreatedDateUtc = DateTime.UtcNow,
                    CreatedBy = null,
                    ModifiedById = null,
                    ModifiedDateUtc = DateTime.UtcNow,
                       CompanyId = context.Companies.First().Id
                }
                 ,
                new ArticleTag
                {
                    Id = 4,
                    Name = "Technology",
                    Description = "",
                    CreatedDateUtc = DateTime.UtcNow,
                    CreatedBy = null,
                    ModifiedById = null,
                    ModifiedDateUtc = DateTime.UtcNow,
                       CompanyId = context.Companies.First().Id
                }
            };
            foreach (var tag in articleTags)
            {
                context.ArticleTags.AddOrUpdate(tag);
             
            }
        }


    }
}
