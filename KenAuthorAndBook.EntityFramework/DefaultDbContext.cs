using KenAuthorAndBook.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace KenAuthorAndBook.EntityFramework
{
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options) { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)                
                .WithMany(a => a.Books)               
                .HasForeignKey(b => b.AuthorId);      

            
            var author1Id = Guid.NewGuid();
            var author2Id = Guid.NewGuid();

            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorId = author1Id, Name = "Jane Austen", Bio = "English novelist known for her six major novels." },
                new Author { AuthorId = author2Id, Name = "Mark Twain", Bio = "American writer, humorist, and lecturer." }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book { BookId = Guid.NewGuid(), Title = "Lambatin mo ang aking fart part one", PublicationDate = new DateTime(1813, 1, 28), AuthorId = author1Id },
                new Book { BookId = Guid.NewGuid(), Title = "Keanu ang paglalakbay", PublicationDate = new DateTime(1884, 12, 10), AuthorId = author2Id }
            );
        }
    }
}
