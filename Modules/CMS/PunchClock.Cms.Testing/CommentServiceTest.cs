using NUnit.Framework;
using PunchClock.Cms.Contract;
using PunchClock.Cms.Model;
using PunchClock.Cms.Service;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace PunchClock.Cms.Testing
{
    public class CommentServiceTest
    {
        private ICommentService _commentService;

        [SetUp]
        public void Initialize()
        {
            _commentService=new CommentService();
        }
        [Test]
        public void Add()
        {
            var comment = new ArticleComment()
            {
                Description = "Test data",
                ArticleId = 1,
                LastModifiedBy = null,
                CompanyId = 1
            };
            var result = _commentService.Add(comment);
            Assert.IsNotNull(result);
        }
        [Test]
        public void Update()
        {
            var comment = new ArticleComment()
            {
                Description = "Test datass",
                ArticleId = 1,
                Id = 1,
                LastModifiedBy = null
            };
            var result = _commentService.Update(comment);
            Assert.IsNotNull(result);
        }

        [TestCase(2)]
        public void Delete(int id)
        {
           var result = _commentService.Delete(id);
            Assert.IsTrue(result.Success);
        }
    }
}
