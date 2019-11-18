using Library.Core.Entities;
using Library.Core.Interfaces;
using Library.Infrastructure.Data;
using Library.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Library.UnitTests
{
    public class TestHelper
    {
        private readonly LibraryDbContext libraryDbContext;
        public TestHelper()
        {
            var builder = new DbContextOptionsBuilder<LibraryDbContext>();
            builder.UseInMemoryDatabase(databaseName: "LibraryDbInMemory");
            var dbContextOptions = builder.Options;

            libraryDbContext = new LibraryDbContext(dbContextOptions);
            libraryDbContext.Database.EnsureDeleted();
            libraryDbContext.Database.EnsureCreated();
        }

        public IGenericReadRepository GetInMemoryReadRepository()
        {
            return new GenericReadRepository(libraryDbContext);
        }

        public IGenericWriteRepository GetInMemoryWriteRepository()
        {
            return new GenericWriteRepository(libraryDbContext);
        }

        public IEnumerable<Author> GetMockAuthors()
        {
            return new List<Author>()
            {
                { new Author(){ Id = 1, FirstName = "William", LastName = "Shakespear"} },
                { new Author(){ Id = 2, FirstName = "Charles", LastName = "Dickens"} },
                { new Author(){ Id = 3, FirstName = "George", LastName = "Eliot"} }
            };
        }
        /// <summary>
        /// Mock data for Books.
        /// Total Records : 3
        /// First Record : BookName = Hamlet
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Book> GetMockBooks()
        {
            return new List<Book>() {
                {new Book()
            {
                Id=1,
                BookName = "Hamlet",
                Author = new Author() { FirstName = "William", LastName = "Shakespear" },
                Category = new Category() { CategoryName = "Drama" }
            } },

                 {new Book()
            {
                Id=2,
                BookName = "Oliver Twist",
                Author = new Author() { FirstName = "Charles", LastName = "Dickens" },
                Category = new Category() { CategoryName = "Novel" }
            } },
                  {new Book()
            {
                Id=3,
                BookName = "Bleak House",
                Author = new Author() { FirstName = "Charles", LastName = "Dickens" },
                Category = new Category() { CategoryName = "Novel" }
            } }
            };
        }
    }
}
