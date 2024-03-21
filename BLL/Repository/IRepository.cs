namespace Data.Repository
{
    public partial interface IRepository<T> where T : class
    {
        public Task<T> CreateAsync(T entity);
        public Task<T> UpdateAsync(int id, T entity);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int id);
        public Task<T> RemoveAsync(int id);
        public Task<IEnumerable<T>> CreateMultipleAsync(IEnumerable<T> entities);
        public Task<IEnumerable<T>> RemoveMultipleAsync(IEnumerable<int> ids);
    }
}
