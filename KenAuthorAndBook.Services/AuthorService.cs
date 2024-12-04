using KenAuthorAndBook.Contracts;
using KenAuthorAndBook.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KenAuthorAndBook.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepository<Author> _repository;

        public AuthorService(IRepository<Author> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Author?>> GetAllAsync()
        {
            // Eager loading Books using Include
            return await _repository.All()
                                     .Include(a => a.Books)  // Eager load Books related to Authors
                                     .ToListAsync();  // Execute query and return the results
        }

        public async Task<Author?> GetByIdAsync(Guid? id)
        {
            if (id == null)
            {
                return null;
            }

            // Eager loading Books using Include for a specific Author
            return await _repository.All()
                                     .Include(a => a.Books)  // Eager load Books
                                     .FirstOrDefaultAsync(a => a.AuthorId == id);  // Find by ID
        }

        public async Task AddAsync(Author author)
        {
            await _repository.AddAsync(author);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(Author author)
        {
            _repository.Update(author);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var author = await GetByIdAsync(id);
            if (author != null)
            {
                _repository.Delete(author);
                await _repository.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Author not found.");
            }
        }
    }
}
