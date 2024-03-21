﻿using Manager_Point.ApplicationDbContext;
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
            await using var transaction = await _dbcontext.Database.BeginTransactionAsync(); // khởi tạo 1 transaction mới
            var obj = await _dbcontext.Set<T>().AddAsync(entity);
            await _dbcontext.SaveChangesAsync();
            await transaction.CommitAsync();
            return obj.Entity;
        }

        public async Task<IEnumerable<T>> CreateMultipleAsync(IEnumerable<T> entities)
        {
            await using var transaction = await _dbcontext.Database.BeginTransactionAsync();
            await _dbcontext.Set<T>().AddRangeAsync(entities);
            await _dbcontext.SaveChangesAsync();
            await transaction.CommitAsync();
            return entities;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            await using var transaction = await _dbcontext.Database.BeginTransactionAsync();
            var obj = await _dbcontext.Set<T>().AsNoTracking().ToListAsync(); // AsNoTracking nó giống kiểu read-only ý
            await transaction.CommitAsync();
            return obj;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            await using var transaction = await _dbcontext.Database.BeginTransactionAsync();
            var obj = await _dbcontext.Set<T>().FindAsync(id);
            await transaction.CommitAsync();
            return obj!;
        }

        public async Task<T> RemoveAsync(int id)
        {
            await using var transaction = await _dbcontext.Database.BeginTransactionAsync();
            var obj = await _dbcontext.Set<T>().FindAsync(id);
            if (obj != null)
            {
                _dbcontext.Set<T>().RemoveRange(obj);
                await _dbcontext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            return obj!;
        }
        public async Task<IEnumerable<T>> RemoveMultipleAsync(IEnumerable<int> ids)
        {
            await using var transaction = await _dbcontext.Database.BeginTransactionAsync();
            var objectsToRemove = await _dbcontext.Set<T>()
                                    .Where(x => ids.Contains(EF.Property<int>(x, "Id")))
                                    .ToListAsync();
            if (objectsToRemove.Any())
            {
                _dbcontext.Set<T>().RemoveRange(objectsToRemove);
                await _dbcontext.SaveChangesAsync();
                await transaction.CommitAsync();
            }

            return objectsToRemove;
        }

        public async Task<T> UpdateAsync(int id, T entity)
        {
            await using var transaction = await _dbcontext.Database.BeginTransactionAsync();
            var obj = await _dbcontext.Set<T>().FindAsync(id);
            if (obj != null)
            {
                _dbcontext.Entry(obj).State = EntityState.Modified;
                _dbcontext.Entry(obj).CurrentValues.SetValues(entity);
                await _dbcontext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            return obj!;
        }
    }
}
