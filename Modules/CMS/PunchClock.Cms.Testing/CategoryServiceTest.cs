using NUnit.Framework;
using PunchClock.Cms.Contract;
using PunchClock.Cms.Model;
using PunchClock.Cms.Service;
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
            var categoryArray = new string[] {"CMS","Ticketing","Admin","RB"};
            foreach (var categoryName in categoryArray)
            {
                var newCategory = new ArticleCategory
                {
                    Name = categoryName,
                    Description = categoryName,
                    LastModifiedBy = null,
                    CompanyId = 1
                };
               var  result= _categoryService.Add(newCategory);
                Assert.IsNotNull(result);
            }
        
            
        }
        [Test]
        public void Update()
        {
           var category = new ArticleCategory
            {
                Id=6,
                Name = "Admin1",
                Description = "Admins data",
                LastModifiedBy = null
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
    }
}
