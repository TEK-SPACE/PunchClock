using System.Collections.Generic;
using NUnit.Framework;
using PunchClock.Cms.Contract;
using PunchClock.Cms.Model;
using PunchClock.Cms.Service;
using PunchClock.Language.Model;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace PunchClock.Cms.Testing
{
   public class TagServiceTest
   {
       private ITagsService _tagService;

       [SetUp]
       public void Initialize()
       {
           _tagService=new TagService();
       }
       [Test]
        public void Add()
        {
            var tag = new ArticleTag()
            {

                Description = "Test data",
                Name = "Test",
                ModifiedById = null,
                CompanyId = 1,
                Resources = new List<ArticleTagResource>
                {
                    new ArticleTagResource { Culture = Culture.English, Value = "Test"},
                    new ArticleTagResource { Culture = Culture.Spanish, Value = "Prueba"},
                    new ArticleTagResource { Culture = Culture.Hindi, Value = "pareekshan"}
                }
            };
            var result = _tagService.Add(tag);
            Assert.IsNotNull(result);
        }
        [Test]
        public void Update()
        {
           var tag = new ArticleTag()
            {
                Id=1,
                Description = "Test11 data",
                Name = "Tests",
                ModifiedById = null
           };
            var result = _tagService.Update(tag);
            Assert.IsNotNull(result);
        }

        [TestCase(2)]
        public void Delete(int id)
        {
           var result = _tagService.Delete(id);
            Assert.IsTrue(result.Success);
        }
        [TestCase(1)]
        public void GetOneArticleTag(int id)
        {
            var result = _tagService.GetOneArticleTag(id);
            Assert.IsNotNull(result);
        }

        [TestCase(1)]
        public void GetArticleTagByCompany(int companyId)
        {
            var result = _tagService.GetArticleTagsByCompany(companyId);
            Assert.IsNotNull(result);
        }
        [Test]
        public void GetAllArticleTags()
        {
            var result = _tagService.GetAllArticleTags();
            Assert.IsNotNull(result);
        }
    }
}
