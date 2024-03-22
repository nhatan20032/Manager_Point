using BLL.ViewModels.Role;
using Manager_Point.Models;
using Microsoft.AspNetCore.Mvc;

namespace BLL.Services.RoleServices.Interface
{
    public interface IRoleServices
    {
        public Task<string> AddItem(VM_Create_Roles request);
        public Task<string> ModifiedItem(int id, Role entity);
        public Task<string> RemoveItem(int id);
        public Task<string> CreateMultipleAsync(List<Role> entities);
        public Task<string> RemoveMultipleAsync(List<int> ids);
        public Task<string> GetAllAsync(int pageNumber, int pageSize, string search);
        public Task<Role> GetByIdAsync(int id);

    }
}
