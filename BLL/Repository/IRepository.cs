namespace BLL.Repository
{
    public partial interface IRepository<T> where T : class
    {
        public Task<T> CreateAsync(T entity);
        public Task<T> UpdateAsync(int id, T entity);
        public Task<List<T>> GetAllAsync(int pageNumber, int pageSize);
        public Task<T> GetByIdAsync(int id);
        public Task<T> RemoveAsync(int id);
        public Task<List<T>> CreateMultipleAsync(List<T> entities);
        public Task<List<T>> RemoveMultipleAsync(List<int> ids);
    }
}
