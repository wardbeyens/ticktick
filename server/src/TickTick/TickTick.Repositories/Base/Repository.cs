using Microsoft.EntityFrameworkCore;
using TickTick.Data;
using TickTick.Models;

namespace TickTick.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly TickTickDbContext _dbContext;
        private readonly DbSet<T> _modelDbSet;

        public Repository(TickTickDbContext ctx)
        {
            _dbContext = ctx ?? throw new ArgumentNullException();
            _modelDbSet = _dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            _modelDbSet.Add(entity);
        }

        // Speciaal omdat we geen delete doen, we doen een soft delete
        public void Delete(T entity)
        {
            Update(entity);
        }

        public IQueryable<T> GetAll()
        {
            return _modelDbSet;
        }
        public async Task<IEnumerable<T>> GetAllAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return await _modelDbSet.Where(predicate).ToListAsync();
        }

        public Task<T> GetAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return _modelDbSet.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            // Entity framework tracking of objects etc, voordat ie daar an kan beginnen, wijsmaken aan in memory db om te tracken, heb ik dat al ja of nee en dan kan ie dat zelf bijhouden welke props dat gewijzigd zijn ja of nee, deze is voor ons gewijzigd
            // Laten weten aan copy om te zeggen dat ie gewijzigd is.
            _modelDbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
