using NUnit.Framework;
using PunchClock.Cms.Contract;
using PunchClock.Cms.Model;
using PunchClock.Cms.Service;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace PunchClock.Cms.Testing
{
   public class ArticleServiceTest
   {
       private IArticleService _articleService;

       [SetUp]
       public void Initialize()
       {
           _articleService=new ArticleService();
       }

        [Test]
        public void Add()
        {
           var article = new Article
            {
                Title = "Test by ajeet",
                Description = "Test data",
                CategoryId = 1,
                 Tags = "title",
                CompanyId = 1
            };
            var record= _articleService.Add(article);
            Assert.IsNotNull(record);
        }
        [Test]
        public void Update()
        {
            var article = new Article
            {
                Id=1,
                Title = "Test by ajeet updated",
                Description = "Test updated",
                CategoryId = 2,
                Tags = "updated",
                ModifiedById = null
            };
            var record = _articleService.Update(article);
            Assert.IsNotNull(record);
        }

        [TestCase(2)]
        public void Delete(int id)
        {
           var result = _articleService.Delete(id);
            Assert.IsTrue(result.Success);
        }

       [TestCase(1)]
       public void GetOneArticle(int id)
       {
           var result = _articleService.GetOneArticle(id);
            Assert.IsNotNull(result);
       }

        [TestCase(1)]
        public void GetArticlesByCompanyId(int companyId)
        {
            var result = _articleService.GetArticlesByCompanyId(companyId);
            Assert.IsNotNull(result);
        }
        [Test]
        public void GetAllArticles()
        {
            var result = _articleService.GetAllArticles();
            Assert.IsNotNull(result);
        }
    }
}
