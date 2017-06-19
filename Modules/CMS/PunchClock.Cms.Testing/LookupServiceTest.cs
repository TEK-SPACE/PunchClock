using NUnit.Framework;
using PunchClock.Cms.Contract;
using PunchClock.Cms.Service;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace PunchClock.Cms.Testing
{
    public class LookupServiceTest
    {
        private ILookupService _lookupService;

        [SetUp]
        public void Initialize()
        {
            _lookupService=new LookupService();
        }

        [Test]
        public void GetAllCategories()
        {
            var categories = _lookupService.GetAllCategories();
            Assert.IsNotNull(categories);
        }
        [TestCase(5)]
        public void GetOneArticle(int id)
        {
            var article = _lookupService.GetOneArticle(id);
            Assert.IsNotNull(article);
        }
    }
}
