using KenAuthorAndBook.Contracts;
using KenAuthorAndBook.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KenAuthorAndBook.MySql
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DefaultDbContext _dbContext;
        private DbSet<TEntity> _dbSet;

        public Repository(DefaultDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task AddAsync(TEntity model)
        {
            await _dbSet.AddAsync(model);
        }

        public IQueryable<TEntity> All()
        {
            return _dbSet.AsNoTracking();
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public void Update(TEntity model)
        {
            _dbSet.Update(model);
        }

        public void Delete(TEntity model)
        {
            _dbSet.Remove(model);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }

}
