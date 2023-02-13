using System.Linq.Expressions;
using TickTick.Models;

namespace TickTick.Repositories.Base
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();

        // Person q = await repo.GetAsync(p => p.IsDeleted == false);
        // var q = repo.GetAsync(p => p.Id == id);
        // var q = repo.GetAsync(p => p.firstName == "Ward" && p.IsDeleted == false);
        // Een heel generieke methode, elk object kan daar gebruik van maken
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        // Number of affected entries
        Task<int> SaveAsync();

        
    }

}
