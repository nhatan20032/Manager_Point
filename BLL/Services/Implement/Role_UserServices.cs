using AutoMapper;
using BLL.Services.Interface;
using BLL.ViewModels.Role_User;
using Manager_Point.ApplicationDbContext;
using Manager_Point.Models;

namespace BLL.Services.Implement
{
	
	public class Role_UserServices : IRole_UserServices
    {
        private readonly AppDbContext _appContext;
        private readonly IMapper _mapper;
        public Role_UserServices(IMapper mapper)
        {
            _appContext = new AppDbContext();
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<List<int>> Batch_Create_Item(List<vm_role_user> requests)
        {
            try
            {
                var obj = _mapper.Map<List<User_Role>>(requests);
                _appContext.Users_Roles.AddRange(obj);
                await _appContext.SaveChangesAsync();
                var ids = obj.Select(t => t.Id).ToList();
                return ids;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Batch_Create_Item: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> Batch_Remove_Item(List<int> ids)
        {
            try
            {
                var role_user = _appContext.Users_Roles.Where(t => ids.Contains(t.Id)).ToList();

                if (role_user.Any())
                {
                    _appContext.Users_Roles.RemoveRange(role_user);
                    await _appContext.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Batch_Remove_Item: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> Batch_Remove_Item_By_UserId(int userIds)
        {
            try
            {
                var role_user = _appContext.Users_Roles.Where(t => t.UserId == userIds).ToList();

                if (role_user.Any())
                {
                    _appContext.Users_Roles.RemoveRange(role_user);
                    await _appContext.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Batch_Remove_Item: {ex.Message}");
                throw;
            }
        }

        public async Task<int> Create_Item(vm_role_user request)
        {
            try
            {
                var obj = _mapper.Map<User_Role>(request);
                _appContext.Users_Roles.AddRange(obj);
                await _appContext.SaveChangesAsync();
                return obj.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Batch_Create_Item: {ex.Message}");
                throw;
            }
        }

        public async Task<int> Modified_Item(int id, vm_role_user request)
        {
            try
            {
                var objForUpdate = await _appContext.Users_Roles.FindAsync(id);
                if (objForUpdate == null) return -1;
                objForUpdate.RoleId = request.RoleId;
                objForUpdate.UserId = request.UserId;
                await _appContext.SaveChangesAsync();
                return objForUpdate.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Modified_Item: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> Remove_Item(int id)
        {
            try
            {
                var objToRemove = await _appContext.Users_Roles.FindAsync(id);
                // Xử lý trường hợp không tìm thấy đối tượng
                if (objToRemove == null) return false;

                // Kiểm tra xem đối tượng đã được theo dõi trong DbContext hay không
                var local = _appContext.Users_Roles.Local.FirstOrDefault(x => x.Id == id);

                // Sử dụng toán tử ba ngôi để xác định đối tượng cần xóa
                _appContext.Users_Roles.Remove(local != null ? local : objToRemove);

                // Lưu các thay đổi vào cơ sở dữ liệu
                await _appContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Remove_Item: {ex.Message}");
                throw;
            }
        }
    }
}
