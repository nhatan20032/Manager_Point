using AutoMapper;
using BLL.Services.Interface;
using BLL.ViewModels.Subject_Teacher;
using Manager_Point.ApplicationDbContext;
using Manager_Point.Models;

namespace BLL.Services.Implement
{
	
	public class Subject_TeacherServices : ISubject_TeacherServices
    {
        private readonly AppDbContext _appContext;
        private readonly IMapper _mapper;
        public Subject_TeacherServices(IMapper mapper)
        {
            _appContext = new AppDbContext();
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<List<int>> Batch_Create_Item(List<vm_subject_teacher> requests)
        {
            try
            {
                var obj = _mapper.Map<List<Subject_Teacher>>(requests);
                _appContext.Subjects_Teachers.AddRange(obj);
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
                var subject_teachers = _appContext.Subjects_Teachers.Where(t => ids.Contains(t.Id)).ToList();

                if (subject_teachers.Any())
                {
                    _appContext.Subjects_Teachers.RemoveRange(subject_teachers);
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

        public async Task<int> Create_Item(vm_subject_teacher request)
        {
            try
            {
                var obj = _mapper.Map<Subject_Teacher>(request);
                _appContext.Subjects_Teachers.AddRange(obj);
                await _appContext.SaveChangesAsync();
                return obj.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Batch_Create_Item: {ex.Message}");
                throw;
            }
        }

        public async Task<int> Modified_Item(int id, vm_subject_teacher request)
        {
            try
            {
                var objForUpdate = await _appContext.Subjects_Teachers.FindAsync(id);
                if (objForUpdate == null) return -1;
                objForUpdate.SubjectId = request.SubjectId;
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
                var objToRemove = await _appContext.Subjects_Teachers.FindAsync(id);
                // Xử lý trường hợp không tìm thấy đối tượng
                if (objToRemove == null) return false;

                // Kiểm tra xem đối tượng đã được theo dõi trong DbContext hay không
                var local = _appContext.Subjects_Teachers.Local.FirstOrDefault(x => x.Id == id);

                // Sử dụng toán tử ba ngôi để xác định đối tượng cần xóa
                _appContext.Subjects_Teachers.Remove(local != null ? local : objToRemove);

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
