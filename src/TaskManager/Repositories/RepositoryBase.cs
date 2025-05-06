using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TaskManager.Interfaces.Repositories;
using TaskManager.Models;

namespace TaskManager.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T>
        where T : class
    {
        protected TaskManagerContext _context;

        protected RepositoryBase(TaskManagerContext context)
        {
            _context = context;
        }

        public void Create(T entity) => _context.Add(entity);

        public void Delete(T entity) => _context.Remove(entity);

        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges ? _context.Set<T>().AsNoTracking() : _context.Set<T>();

        public IQueryable<T> FindByCondition(
            Expression<Func<T, bool>> expression,
            bool trackChanges
        ) =>
            !trackChanges
                ? _context.Set<T>().Where(expression).AsNoTracking()
                : _context.Set<T>().Where(expression);

        public void Update(T entity) => _context.Update(entity);
    }
}
