using B2BMart.DataLayer.Repositories.Specifications;
using System.Linq.Expressions;

namespace B2BMart.DataLayer.Repositories
{
    public interface IGenericRepository<T>
    {
        T Add(T entity);
        void Delete(T entity);
        void AddRange(IEnumerable<T> entity);
        T Find(int id);
        IQueryable<T> FindAsQueryable(Expression<Func<T, bool>> predicate, bool isTrackingRequired = true);
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        T Remove(T entity);
        void SaveChanges();

    }
}