using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using PunchClock.Cms.Model;

namespace PunchClock.Core.DataAccess.Seeders
{
    public partial class PunchDbInitializer
    {
        private void SeedArticleCategory(PunchClockDbContext context)
        {
            var articleCategories = new List<ArticleCategory>
            {
                new ArticleCategory
                {
                   Name = "Admin",
                    Description = "",
                    CreatedDateUtc = DateTime.UtcNow,
                    CreatedBy = null,
                    ModifiedById = null,
                    ModifiedDateUtc = DateTime.UtcNow,
                    CompanyId = 1
                },
                new ArticleCategory
                {
                    Name = "RBTek",
                    Description = "",
                   CreatedDateUtc = DateTime.UtcNow,
                    CreatedBy = null,
                    ModifiedById = null,
                    ModifiedDateUtc = DateTime.UtcNow,
                       CompanyId = 1
                }
                ,
                new ArticleCategory
                {
                    Name = "CMS",
                    Description = "",
                    CreatedDateUtc = DateTime.UtcNow,
                    CreatedBy = null,
                    ModifiedById = null,
                    ModifiedDateUtc = DateTime.UtcNow,
                       CompanyId = 1
                }
                 ,
                new ArticleCategory
                {
                    Name = "Ticketing",
                    Description = "",
                     CreatedDateUtc = DateTime.UtcNow,
                    CreatedBy = null,
                    ModifiedById = null,
                    ModifiedDateUtc = DateTime.UtcNow,
                       CompanyId = 1
                }
            };
            foreach (var category in articleCategories)
            {
                context.ArticleCategrories.AddOrUpdate(category);
          }
        }

    }
}
