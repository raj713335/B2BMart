using B2BMart.DataLayer.Models;
using B2BMart.DataLayer.Repositories.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace B2BMart.DataLayer.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly B2BMartContext _context;
        public GenericRepository(B2BMartContext context)
        {
            _context = context;
        }

        public virtual T Add(T entity)
        {
            return _context
                .Add(entity)
                .Entity;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public virtual void AddRange(IEnumerable<T> entity)
        {
            _context.AddRange(entity);
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public virtual T Find(int id)
        {
            return _context.Find<T>(id);
        }

        public virtual IQueryable<T> FindAsQueryable(Expression<Func<T, bool>> predicate, bool isTrackingRequired = true)
        {
            return
            isTrackingRequired == true
                ? _context.Set<T>()
                        .AsQueryable()
                        .Where(predicate)
                : _context.Set<T>()
                        .AsQueryable()
                        .AsNoTracking()
                        .Where(predicate);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public virtual T Remove(T entity)
        {
            return _context.Remove(entity)
                .Entity;
        }

        public virtual void SaveChanges()
        {
            _context.SaveChanges();
        }

        #region private methods

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }

        #endregion
    }
}
