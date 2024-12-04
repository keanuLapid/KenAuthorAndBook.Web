using KenAuthorAndBook.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace KenAuthorAndBook.EntityFramework
{
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Author>? Authors { get; set; }
        public DbSet<Book>? Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Create unique GUIDs for Authors and Books
            Guid authorId1 = Guid.NewGuid();
            Guid authorId2 = Guid.NewGuid();
            Guid authorId3 = Guid.NewGuid();

            Guid bookId1 = Guid.NewGuid();
            Guid bookId2 = Guid.NewGuid();
            Guid bookId3 = Guid.NewGuid();

            // List of Authors
            List<Author> authors = new List<Author>
            {
                new Author
                {
                    AuthorId = authorId1,
                    Name = "J.K. Rowling",
                    Bio = "British author, best known for the Harry Potter series."
                },
                new Author
                {
                    AuthorId = authorId2,
                    Name = "George R. R. Martin",
                    Bio = "American novelist and short story writer, author of the A Song of Ice and Fire series."
                },
                new Author
                {
                    AuthorId = authorId3,
                    Name = "J.R.R. Tolkien",
                    Bio = "English author, best known for The Lord of the Rings series."
                }
            };

            // List of Books
            List<Book> books = new List<Book>
            {
                new Book
                {
                    BookId = bookId1,
                    Title = "Harry Potter and the Philosopher's Stone",
                    PublicationDate = new DateTime(1997, 6, 26),
                    AuthorId = authorId1
                },
                new Book
                {
                    BookId = bookId2,
                    Title = "A Game of Thrones",
                    PublicationDate = new DateTime(1996, 8, 6),
                    AuthorId = authorId2
                },
                new Book
                {
                    BookId = bookId3,
                    Title = "The Hobbit",
                    PublicationDate = new DateTime(1937, 9, 21),
                    AuthorId = authorId3
                }
            };

            // Seeding Authors and Books data
            modelBuilder.Entity<Author>().HasData(authors);
            modelBuilder.Entity<Book>().HasData(books);

            // Define Many-to-Many relationship between Authors and Books
            modelBuilder.Entity<Author>()
                .HasMany(a => a.Books)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId);
        }
    }
}
