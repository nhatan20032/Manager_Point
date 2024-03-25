using AutoMapper;
using BLL.Services.Interface;
using BLL.ViewModels;
using BLL.ViewModels.User;
using Manager_Point.ApplicationDbContext;
using Manager_Point.Models;
using Newtonsoft.Json;
using System.Data.Entity;
using OfficeOpenXml;

namespace BLL.Services.Implement
{
    public class UserServices : IUserServices
    {
        private readonly AppDbContext _appContext;
        private readonly IMapper _mapper;
        public UserServices(IMapper mapper)
        {
            _appContext = new AppDbContext();
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<List<int>> Batch_Create_Item(List<vm_create_user> requests)
        {
            try
            {
                var obj = _mapper.Map<List<User>>(requests);
                _appContext.Users.AddRange(obj);
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
                var userDelete = await _appContext.Users.Where(t => ids.Contains(t.Id)).ToListAsync();

                if (userDelete.Any())
                {
                    _appContext.Users.RemoveRange(userDelete);
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

        public async Task<int> Create_Item(vm_create_user request)
        {
            try
            {
                var obj = _mapper.Map<User>(request);
                _appContext.Users.AddRange(obj);
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
                var query = _appContext.Users
                    .Where(t => string.IsNullOrEmpty(search) || t.Name!.Contains(search))
                    .Skip(skip)
                    .Take(page_size);
                var roles = query.ToList();

                int totalCount = _appContext.Users
                    .Where(s => string.IsNullOrEmpty(search) || s.Name!.Contains(search))
                    .Count();

                var vm_User = _mapper.Map<List<vm_user>>(roles);
                var paginatedResult = new PaginatedResult<vm_user>
                {
                    TotalCount = totalCount,
                    PageNumber = page_number,
                    PageSize = page_size,
                    Data = vm_User
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

        public async Task<vm_user> Get_By_Id(int id)
        {
            try
            {
                var existingSubject = await _appContext.Users.FindAsync(id); // kiểm tra trog db context có không thì lấy luôn
                if (existingSubject != null)
                {
                    var vm_user = _mapper.Map<vm_user>(existingSubject);
                    return vm_user;
                }
                var user = await _appContext.Users.FirstOrDefaultAsync(s => s.Id == id); // không thì truy cập vào db để lấy đối tượng ra
                if (user == null) return null!;
                var vm_user_fromDb = _mapper.Map<vm_user>(user);
                return vm_user_fromDb;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Get_By_Id: {ex.Message}");
                throw;
            }
        }

        public async Task<int> Modified_Item(int id, vm_update_user request)
        {
            try
            {
                var objForUpdate = await _appContext.Users.FindAsync(id);
                if (objForUpdate == null) return -1;
                objForUpdate.AvatarUrl = request.AvatarUrl;
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
                var objToRemove = await _appContext.Users.FindAsync(id);
                // Xử lý trường hợp không tìm thấy đối tượng
                if (objToRemove == null) return false;

                // Kiểm tra xem đối tượng đã được theo dõi trong DbContext hay không
                var local = _appContext.Users.Local.FirstOrDefault(x => x.Id == id);

                // Sử dụng toán tử ba ngôi để xác định đối tượng cần xóa
                _appContext.Users.Remove(local != null ? local : objToRemove);

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
        public async Task<int> AddUsersFromExcel(Stream excelFileStream)
        {
            try
            {
                using (var package = new ExcelPackage(excelFileStream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Chọn sheet đầu tiên
                    int rowCount = worksheet.Dimension.Rows;

                    List<vm_create_user> usersToAdd = new();

                    for (int row = 2; row <= rowCount; row++) // Bắt đầu từ dòng thứ 2, vì dòng đầu tiên là header
                    {
                        var user = new vm_create_user
                        {
                            Name = worksheet.Cells[row, 1].Value?.ToString(), // Cột 1 là tên
                            Email = worksheet.Cells[row, 2].Value?.ToString(), // Cột 2 là email
                            User_Code = worksheet.Cells[row, 3].Value.ToString() ?? "null",
                            Password = worksheet.Cells[row, 4].Value.ToString() ?? "null"
                        };

                        usersToAdd.Add(user);
                    }
                    var obj = _mapper.Map<List<User>>(usersToAdd);
                    _appContext.Users.AddRange(obj);
                    await _appContext.SaveChangesAsync();
                    return usersToAdd.Count;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AddUsersFromExcel: {ex.Message}");
                throw;
            }
        }
    }
}
