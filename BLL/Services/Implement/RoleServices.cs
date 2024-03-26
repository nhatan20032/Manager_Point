﻿using AutoMapper;
using BLL.Services.Interface;
using BLL.ViewModels;
using BLL.ViewModels.Role;
using Manager_Point.ApplicationDbContext;
using Manager_Point.Models;
using Newtonsoft.Json;
using System.Data.Entity;

namespace BLL.Services.Implement
{
    public class RoleServices : IRoleServices
    {
        private readonly AppDbContext _appContext;
        private readonly IMapper _mapper;
        public RoleServices(IMapper mapper)
        {
            _appContext = new AppDbContext();
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<List<int>> Batch_Create_Item(List<vm_create_role> requests)
        {
            try
            {
                var obj = _mapper.Map<List<Role>>(requests);
                _appContext.Roles.AddRange(obj);
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
                var rolessToDelete = _appContext.Roles.Where(t => ids.Contains(t.Id)).ToList();

                if (rolessToDelete.Any())
                {
                    _appContext.Roles.RemoveRange(rolessToDelete);
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


        public async Task<int> Create_Item(vm_create_role request)
        {
            try
            {
                var obj = _mapper.Map<Role>(request);
                _appContext.Roles.AddRange(obj);
                await _appContext.SaveChangesAsync();
                return obj.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Batch_Create_Item: {ex.Message}");
                throw;
            }
        }

        public async Task<string> Get_All_Async(int page_number = 1, int page_size = 10, string search = "")
        {
            try
            {
                int skip = (page_number - 1) * page_size;
                var query = _appContext.Roles
                    .Where(t => string.IsNullOrEmpty(search) || t.Name!.Contains(search))
                    .Skip(skip)
                    .Take(page_size);
                var roles = query.ToList();

                int totalCount = _appContext.Roles
                    .Where(s => string.IsNullOrEmpty(search) || s.Name!.Contains(search))
                    .Count();

                var vm_Roles = _mapper.Map<List<vm_role>>(roles);
                var paginatedResult = new PaginatedResult<vm_role>
                {
                    TotalCount = totalCount,
                    PageNumber = page_number,
                    PageSize = page_size,
                    Data = vm_Roles
                };

                var jsonResult = JsonConvert.SerializeObject(paginatedResult, Formatting.Indented);
                return jsonResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Get_All_Async: {ex.Message}");
                throw;
            }
        }

        public async Task<vm_role> Get_By_Id(int id)
        {
            try
            {
                var existingSubject = await _appContext.Roles.FindAsync(id); // kiểm tra trog db context có không thì lấy luôn
                if (existingSubject != null)
                {
                    var vm_Role = _mapper.Map<vm_role>(existingSubject);
                    return vm_Role;
                }
                var role = await _appContext.Roles.FirstOrDefaultAsync(s => s.Id == id); // không thì truy cập vào db để lấy đối tượng ra
                if (role == null) return null!;
                var vm_role_fromDb = _mapper.Map<vm_role>(role);
                return vm_role_fromDb;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Get_By_Id: {ex.Message}");
                throw;
            }
        }

        public async Task<int> Modified_Item(int id, vm_update_role request)
        {
            try
            {
                var objForUpdate = await _appContext.Roles.FindAsync(id);
                if (objForUpdate == null) return -1;
                objForUpdate.Description = request.Description;
                objForUpdate.Name = request.Name;
                objForUpdate.Status = request.Status;
                // Không cần gọi Attach hoặc Update vì objForUpdate đã được theo dõi trong DbContext neuse có sự thay đổi thì nó sẽ cập nhật vô DB
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
                var objToRemove = await _appContext.Roles.FindAsync(id);
                // Xử lý trường hợp không tìm thấy đối tượng
                if (objToRemove == null) return false;

                // Kiểm tra xem đối tượng đã được theo dõi trong DbContext hay không
                var local = _appContext.Roles.Local.FirstOrDefault(x => x.Id == id);

                // Sử dụng toán tử ba ngôi để xác định đối tượng cần xóa
                _appContext.Roles.Remove(local != null ? local : objToRemove);

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
