using KenAuthorAndBook.Contracts;
using KenAuthorAndBook.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenAuthorAndBook.Services
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _repository;

        public BookService(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(Book? entity)
        {
            if (entity != null)
            {
                await _repository.AddAsync(entity);
                await _repository.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            Book? entity = _repository.All().FirstOrDefault(a => a.BookId == id);

            if (entity != null)
            {
                _repository.Delete(entity);
                await _repository.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Book?>> GetAllAsync()
        {
            return await _repository.All().ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(Guid? id)
        {
            return await _repository.All().FirstOrDefaultAsync(a => a.BookId == id);
        }



        public async Task UpdateAsync(Book? entity)
        {
            if (entity != null)
            {
                Book? oldEntity = _repository.All().FirstOrDefault(a => a.BookId == entity.BookId);

                if (oldEntity != null)
                {
                    oldEntity.Title = entity.Title;
                    oldEntity.Author = entity.Author;

                    _repository.Update(oldEntity);
                    await _repository.SaveChangesAsync();
                }
            }
        }
    }
}
