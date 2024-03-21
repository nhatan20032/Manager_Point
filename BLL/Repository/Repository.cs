using Manager_Point.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public partial class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _dbcontext;
        public Repository()
        {
            _dbcontext = new AppDbContext();
        }
        public async Task<T> CreateAsync(T entity)
        {
            var obj = await _dbcontext.Set<T>().AddAsync(entity);
            await _dbcontext.SaveChangesAsync();
            return obj.Entity;
        }

        public async Task<List<T>> GetAllAsync()
        {
            var obj = await _dbcontext.Set<T>().ToListAsync();
            return obj;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var obj = await _dbcontext.Set<T>().FindAsync(id);
            return obj;
        }

        public async Task<T> RemoveAsync(int id)
        {
            var obj = await _dbcontext.Set<T>().FindAsync(id);
            if (obj != null)
            {
                _dbcontext.Set<T>().Remove(obj);
                await _dbcontext.SaveChangesAsync();
            }
            return obj;
        }

        public async Task<T> RemoveAsync(T obj)
        {
            if (obj != null)
            {
                _dbcontext.Set<T>().Remove(obj);
                await _dbcontext.SaveChangesAsync();
            }
            return obj;
        }

        public async Task<T> UpdateAsync(int id, T entity)
        {
            var existingEntity = await _dbcontext.Set<T>().FindAsync(id);

            if (existingEntity != null)
            {
                _dbcontext.Entry(existingEntity).CurrentValues.SetValues(entity);
                await _dbcontext.SaveChangesAsync();
            }

            return existingEntity;
        }
    }
}
