using Library.Core.Specifications.Authors;
using Library.Core.Specifications.Books;
using System.Linq;
using Xunit;

namespace Library.UnitTests
{
    public class SpecificationTests
    {
        [Fact]
        public void BookWithAuthorAndCategorySpecification_UseSpec()
        {
            var helper = new TestHelper();
            var mockBooks = helper.GeMockBooks().AsQueryable();
            var spec = new BookWithAuthorAndCategorySpecification("Bleak");

            var result = mockBooks.FirstOrDefault(spec.Criteria);

            Assert.NotNull(result);
            Assert.Equal("Bleak House", result.BookName);
            Assert.Equal("Charles", result.Author.FirstName);
            Assert.Equal("Novel", result.Category.CategoryName);
        }
        [Fact]
        public void AuthorSpecification_UseSpec()
        {
            var helper = new TestHelper();
            var mockAuthors = helper.GetMockAuthors().AsQueryable();
            var spec = new AuthorSpecification("William");

            var result = mockAuthors.FirstOrDefault(spec.Criteria);

            Assert.NotNull(result);
            Assert.Equal("William", result.FirstName);
            Assert.Equal("Shakespear", result.LastName);
        }
    }
}
