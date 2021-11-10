using Microsoft.EntityFrameworkCore;
using SampleProject.Core.Interfaces.Repository;
using SampleProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseModel
    {
        protected readonly DbContext _dbContext;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public TEntity Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            return entity;
        }

        public async Task<bool> Any(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().AnyAsync(predicate);
        }

        public async Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity> Get(int id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<IQueryable<TEntity>> GetAll()
        {
            return await Task.FromResult(_dbContext.Set<TEntity>());
        }

        public void Remove(TEntity entity)
        {
            _dbContext.Remove(entity);
            
        }

        public TEntity Update(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public async Task<IQueryable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.FromResult(_dbContext.Set<TEntity>().Where(predicate));
        }

        public async Task<int> SaveChanges()
        {
            return await _dbContext.SaveChangesAsync();
        }

    }
}
