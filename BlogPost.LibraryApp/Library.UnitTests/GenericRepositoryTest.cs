using Library.Core.Entities;
using Library.Core.Specifications.Books;
using Xunit;
using Library.Core.Specifications.Authors;
using System.Linq;

namespace Library.UnitTests
{
    public class GenericRepositoryTest
    {
        [Fact]
        public void SaveAsync_Book_RightRecord()
        {
            var helper = new TestHelper();

            // Repositories with InMemory Database
            var readyRepo = helper.GetInMemoryReadRepository();
            var writeRepo = helper.GetInMemoryWriteRepository();

            // use Write Repository to add mock data
            writeRepo.AddAsync(new Book()
            {
                BookName = "Hamlet",
                Author = new Author() { FirstName = "William", LastName = "Shakespear" },
                Category = new Category() { CategoryName = "Drama" }
            });

            // Commit insert
            writeRepo.SaveAsyn().GetAwaiter();

            // use Specification in Read Repository and get data
            var result = readyRepo.GetAsync(new
                BookWithAuthorAndCategorySpecification()).Result;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Hamlet", result.BookName);
            Assert.Equal("William", result.Author.FirstName);
            Assert.Equal("Shakespear", result.Author.LastName);
            Assert.Equal("Drama", result.Category.CategoryName);
        }

        [Fact]
        public void GetAll_Authors_Count()
        {
            var helper = new TestHelper();

            var readyRepo = helper.GetInMemoryReadRepository();
            var writeRepo = helper.GetInMemoryWriteRepository();

            var authors = helper.GetMockAuthors();

            writeRepo.InsertAsync(authors).GetAwaiter();

            var result = readyRepo.GetAll(new AuthorSpecification()).ToList();
            Assert.Equal(3, result.Count);
        }
        [Fact]
        public void GetAsync_Authors_Find()
        {
            var helper = new TestHelper();

            var readyRepo = helper.GetInMemoryReadRepository();
            var writeRepo = helper.GetInMemoryWriteRepository();

            var authors = helper.GetMockAuthors();

            writeRepo.InsertAsync(authors).GetAwaiter();

            var result = readyRepo.GetAsync(new AuthorSpecification("William")).Result;
            Assert.Equal("Shakespear", result.LastName);
        }

        [Fact]
        public void GetByIdAsync_Books_RightName()
        {
            var helper = new TestHelper();

            var readyRepo = helper.GetInMemoryReadRepository();
            var writeRepo = helper.GetInMemoryWriteRepository();

            var books = helper.GetMockBooks();

            writeRepo.InsertAsync(books).GetAwaiter();

            var result = readyRepo.GetByIdAsync<Book>(2).Result;
            Assert.Contains("Oliver", result.BookName);
        }

    }
}
