using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Core.Interfaces.Repository
{
    public interface IRepository<TEntity>
    {
        Task<TEntity> Get(int id);
        Task<IQueryable<TEntity>> GetAll();
        TEntity Add(TEntity entity);
        void Remove(TEntity entity);
        TEntity Update(TEntity entity);
        Task<IQueryable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        Task<bool> Any(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
    }
}
