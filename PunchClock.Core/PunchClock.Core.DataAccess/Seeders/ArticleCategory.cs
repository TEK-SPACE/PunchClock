using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
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
                   Name = "SuperAdmin",
                    Description = "",
                    CreatedDateUtc = DateTime.UtcNow,
                    CreatedBy = null,
                    ModifiedById = null,
                    ModifiedDateUtc = DateTime.UtcNow,
                    CompanyId = context.Companies.First().Id
                },
                new ArticleCategory
                {
                    Name = "RBTek",
                    Description = "",
                   CreatedDateUtc = DateTime.UtcNow,
                    CreatedBy = null,
                    ModifiedById = null,
                    ModifiedDateUtc = DateTime.UtcNow,
                       CompanyId = context.Companies.First().Id
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
                       CompanyId = context.Companies.First().Id
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
                       CompanyId = context.Companies.First().Id
                }
            };
            foreach (var category in articleCategories)
            {
                context.ArticleCategrories.AddOrUpdate(category);
          }
        }

    }
}
