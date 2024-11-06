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
            return await _repository.All().ToListAsync();
        }

        public async Task<Author?> GetByIdAsync(Guid? id)
        {
            if (id == null)
            {
                return null; 
            }

            return await _repository.All().FirstOrDefaultAsync(a => a.AuthorId == id);
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
