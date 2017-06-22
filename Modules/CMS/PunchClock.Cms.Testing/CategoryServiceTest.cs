using System.Collections.Generic;
using NUnit.Framework;
using PunchClock.Cms.Contract;
using PunchClock.Cms.Model;
using PunchClock.Cms.Service;
using PunchClock.Language.Model;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace PunchClock.Cms.Testing
{
   
    public class CategoryServiceTest
    {
        private ICategoryService _categoryService;

        [SetUp]
        public void Initialize()
        {
            _categoryService=new CategoryService();
        }
        [Test]
        public void Add()
        {
            var newCategory = new ArticleCategory
            {
                Name = "Laptop",
                Description = "",
                ModifiedById = null,
                CompanyId = 1,
                Resources = new List<ArticleCategoryResource>
                {
                    new ArticleCategoryResource { Culture = Culture.English, Value = "Laptop"},
                    new ArticleCategoryResource { Culture = Culture.Spanish, Value = "SpanishLaptop"},
                    new ArticleCategoryResource { Culture = Culture.Hindi, Value = "Mujhe Nahi Patha"}
                }
            };
            var result = _categoryService.Add(newCategory);
            Assert.IsNotNull(result);
        }
        [Test]
        public void Update()
        {
           var category = new ArticleCategory
            {
                Id=6,
                Name = "Admin1",
                Description = "Admins data",
                ModifiedById = null
           };
            var result = _categoryService.Update(category);
            Assert.IsNotNull(result);
        }

        [TestCase(2)]
        public void Delete(int id)
        {
        var result = _categoryService.Delete(id);
            Assert.IsTrue(result.Success);
        }


        [TestCase(1)]
        public void GetOneArticleCategory(int id)
        {
            var result = _categoryService.GetOneArticleCategory(id);
            Assert.IsNotNull(result);
        }

        [TestCase(1)]
        public void GetCategoriesByCompanyId(int companyId)
        {
            var result = _categoryService.GetArticleCategoriesByCompanyId(companyId);
            Assert.IsNotNull(result);
        }
        [Test]
        public void GetAllArticleCategories()
        {
            var result = _categoryService.GetAllArticleCategories();
            Assert.IsNotNull(result);
        }
    }
}
