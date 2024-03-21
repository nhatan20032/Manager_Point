using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public partial interface IRepository<T> where T : class
	{
		public Task<T> CreateAsync(T entity);
		public Task<T> UpdateAsync(int id, T entity);
		public Task<List<T>> GetAllAsync();
		public Task<T> GetByIdAsync(int id);
		public Task<T> RemoveAsync(int id);
		public Task<T> RemoveAsync(T obj);
	}
}
