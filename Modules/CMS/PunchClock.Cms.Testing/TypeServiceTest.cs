using System.Collections.Generic;
using NUnit.Framework;
using PunchClock.Cms.Contract;
using PunchClock.Cms.Model;
using PunchClock.Cms.Service;
using PunchClock.Language.Model;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace PunchClock.Cms.Testing
{
   public class TypeServiceTest
   {
       private ITypeService _typeService;
       [SetUp]
        public void Initialize()
        {
            _typeService = new TypeService();
        }

        [Test]
        public void Add()
        {
            var type = new ArticleType()
            {
                Description = "Test",
                Name = "Test",
                ModifiedById = null,
                CompanyId = 1,
                Resources = new List<ArticleTypeResource>
                {
                    new ArticleTypeResource { Culture = Culture.English, Value = "Test"},
                    new ArticleTypeResource { Culture = Culture.Spanish, Value = "Prueba"},
                    new ArticleTypeResource { Culture = Culture.Hindi, Value = "pareekshan"}
                }
            };
            var result = _typeService.Add(type);
            Assert.IsNotNull(result);
        }
        [Test]
        public void Update()
        {
            var type = new ArticleType()
            {
                Id=1,
                Description = "Test11 data",
                Name = "Tests",
                ModifiedById = null
            };
            var result = _typeService.Update(type);
            Assert.IsNotNull(result);
        }

        [TestCase(2)]
        public void Delete(int id)
        {
           var result = _typeService.Delete(id);
            Assert.IsTrue(result.Success);
        }
        [TestCase(1)]
        public void GetOneArticleType(int id)
        {
            var result = _typeService.GetOneArticleType(id);
            Assert.IsNotNull(result);
        }

        [TestCase(1)]
        public void GetArticleTypesBbyCompanyId(int companyId)
        {
            var result = _typeService.GetArticleTypesBbyCompanyId(companyId);
            Assert.IsNotNull(result);
        }
        [Test]
        public void GetAllArticleTypes()
        {
            var result = _typeService.GetAllArticleTypes();
            Assert.IsNotNull(result);
        }

    }
}
