using AutoMapper;
using BLL.Services.Interface;
using BLL.ViewModels.Teacher_Class;
using Manager_Point.ApplicationDbContext;
using Manager_Point.Models;

namespace BLL.Services.Implement
{
	public interface ITeacher_ClassServices
	{
		public Task<int> Create_Item(vm_teacher_class request);
		public Task<List<int>> Batch_Create_Item(List<vm_teacher_class> requests);
		public Task<int> Modified_Item(int id, vm_teacher_class request);
		public Task<bool> Remove_Item(int id);
		public Task<bool> Batch_Remove_Item(List<int> ids);
	}
	public class Teacher_ClassServices : ITeacher_ClassServices
    {
        private readonly AppDbContext _appContext;
        private readonly IMapper _mapper;
        public Teacher_ClassServices(IMapper mapper)
        {
            _appContext = new AppDbContext();
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<List<int>> Batch_Create_Item(List<vm_teacher_class> requests)
        {
            try
            {
                var obj = _mapper.Map<List<Teacher_Class>>(requests);
                _appContext.Teacher_Classes.AddRange(obj);
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
                var teacher_class = _appContext.Teacher_Classes.Where(t => ids.Contains(t.Id)).ToList();

                if (teacher_class.Any())
                {
                    _appContext.Teacher_Classes.RemoveRange(teacher_class);
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

        public async Task<int> Create_Item(vm_teacher_class request)
        {
            try
            {
                var obj = _mapper.Map<Teacher_Class>(request);
                _appContext.Teacher_Classes.AddRange(obj);
                await _appContext.SaveChangesAsync();
                return obj.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Batch_Create_Item: {ex.Message}");
                throw;
            }
        }

        public async Task<int> Modified_Item(int id, vm_teacher_class request)
        {
            try
            {
                var objForUpdate = await _appContext.Teacher_Classes.FindAsync(id);
                if (objForUpdate == null) return -1;
                objForUpdate.ClassId = request.ClassId;
                objForUpdate.UserId = request.UserId;
                objForUpdate.TypeTeacher = request.TypeTeacher;
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
                var objToRemove = await _appContext.Teacher_Classes.FindAsync(id);
                // Xử lý trường hợp không tìm thấy đối tượng
                if (objToRemove == null) return false;

                // Kiểm tra xem đối tượng đã được theo dõi trong DbContext hay không
                var local = _appContext.Teacher_Classes.Local.FirstOrDefault(x => x.Id == id);

                // Sử dụng toán tử ba ngôi để xác định đối tượng cần xóa
                _appContext.Teacher_Classes.Remove(local != null ? local : objToRemove);

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
