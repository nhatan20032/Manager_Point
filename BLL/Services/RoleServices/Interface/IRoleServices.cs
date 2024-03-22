using Manager_Point.Models;

namespace BLL.Services.RoleServices.Interface
{
    public interface IRoleServices
    {
        public Task<Role> AddItem(Role request);
        public Task<Role> ModifiedItem(int id, Role entity);
        public Task<Role> RemoveItem(int id);
        public Task<List<Role>> CreateMultipleAsync(List<Role> entities);
        public Task<List<Role>> RemoveMultipleAsync(List<int> ids);
        public Task<string> GetAllAsync(int pageNumber, int pageSize, string search);
        public Task<Role> GetByIdAsync(int id);

    }
}
