using AutoMapper;
using AutoMapper.QueryableExtensions;
using BLL.Services.Interface;
using BLL.ViewModels;
using BLL.ViewModels.Message;
using Manager_Point.ApplicationDbContext;
using Manager_Point.Models;
using Newtonsoft.Json;
using System.Data.Entity;

namespace BLL.Services.Implement
{
	
	public class MessageServices : IMessageServices
    {
        private readonly AppDbContext _appContext;
        private readonly IMapper _mapper;
        public MessageServices(IMapper mapper)
        {
            _appContext = new AppDbContext();
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<List<int>> Batch_Create_Item(List<vm_create_message> requests)
        {
            try
            {
                var obj = _mapper.Map<List<Message>>(requests);
                _appContext.Messages.AddRange(obj);
                await _appContext.SaveChangesAsync();
                var ids = obj.Select(t => t.Id).ToList();
                return ids;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in  Batch_Create_Item: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> Batch_Remove_Item(List<int> ids)
        {
            try
            {
                var messageToDelete = await _appContext.Messages.Where(t => ids.Contains(t.Id)).ToListAsync();

                if (messageToDelete.Any())
                {
                    _appContext.Messages.RemoveRange(messageToDelete);
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

        public async Task<int> Create_Item(vm_create_message request)
        {
            try
            {
                var obj = _mapper.Map<Message>(request);
                _appContext.Messages.AddRange(obj);
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
                var query = _appContext.Classes
                    .Where(t => string.IsNullOrEmpty(search) || t.Name!.Contains(search))
                    .Skip(skip)
                    .Take(page_size);
                var subjects = query.ToList();

                int totalCount = _appContext.Classes
                    .Where(s => string.IsNullOrEmpty(search) || s.Name!.Contains(search))
                    .Count();

                var vm_message = _mapper.Map<List<vm_message>>(_mapper.ConfigurationProvider).ToList();
                var paginatedResult = new PaginatedResult<vm_message>
                {
                    TotalCount = totalCount,
                    PageNumber = page_number,
                    PageSize = page_size,
                    Data = vm_message
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

        public async Task<vm_message> Get_By_Id(int id)
        {
            try
            {
                var message = await _appContext.GradePoints.ProjectTo<vm_message>(_mapper.ConfigurationProvider).SingleOrDefaultAsync(x => x.Id == id);
                if (message == null) return null!;
                return message;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Get_By_Id: {ex.Message}");
                throw;
            }
        }

        public async Task<int> Modified_Item(int id, vm_update_message request)
        {
            try
            {
                var objForUpdate = await _appContext.Messages.FindAsync(id);
                if (objForUpdate == null) return -1;
                objForUpdate.ClassId = request.ClassId;
                objForUpdate.Status = request.Status;
                objForUpdate.Content = request.Content;

                // Không cần gọi Attach hoặc Update vì objForUpdate đã được
                // theo dõi trong DbContext neuse có sự thay đổi thì nó sẽ cập nhật vô DB
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
                var objToRemove = await _appContext.Messages.FindAsync(id);
                // Xử lý trường hợp không tìm thấy đối tượng
                if (objToRemove == null) return false;

                // Kiểm tra xem đối tượng đã được theo dõi trong DbContext hay không
                var local = _appContext.Messages.Local.FirstOrDefault(x => x.Id == id);

                // Sử dụng toán tử ba ngôi để xác định đối tượng cần xóa
                _appContext.Messages.Remove(local != null ? local : objToRemove);

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
