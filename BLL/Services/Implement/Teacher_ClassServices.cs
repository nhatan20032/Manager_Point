using AutoMapper;
using BLL.Services.Interface;
using BLL.ViewModels.Teacher_Class;
using Manager_Point.ApplicationDbContext;
using Manager_Point.Models;
using Manager_Point.Models.Enum;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BLL.Services.Implement
{

    public class Teacher_ClassServices : ITeacher_ClassServices
    {
        private readonly AppDbContext _appContext;
        private readonly IMapper _mapper;
        public Teacher_ClassServices(IMapper mapper)
        {
            _appContext = new AppDbContext();
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<string> Batch_Create_Item_HomeRoom(List<vm_teacher_class> requests)
        {
            try
            {
                var obj = _mapper.Map<List<Teacher_Class>>(requests);
                var addHomeRoom = new List<Teacher_Class>();

                foreach (var teacherClass in obj)
                {
                    // Kiểm tra xem lớp đã có giáo viên chủ nhiệm chưa
                    bool hasExistingHomeRoom = await _appContext.Teacher_Classes
                        .AnyAsync(tc => tc.ClassId == teacherClass.ClassId && tc.TypeTeacher == TypeTeacher.Homeroom_Teacher);

                    // Nếu lớp đã có giáo viên chủ nhiệm, bỏ qua
                    if (hasExistingHomeRoom)
                    {
                        return $"Lớp {teacherClass.ClassId} đã có giáo viên chủ nhiệm.";
                    }

                    // Nếu lớp chưa có giáo viên chủ nhiệm, thêm vào danh sách để thêm mới
                    addHomeRoom.Add(teacherClass);
                }

                // Thêm danh sách giáo viên chủ nhiệm vào context
                _appContext.Teacher_Classes.AddRange(addHomeRoom);
                await _appContext.SaveChangesAsync();

                var ids = addHomeRoom.Select(t => t.Id).ToList();
                var response = new
                {
                    Message = "Giáo viên chủ nhiệm đã được thêm thành công.",
                    AddedIds = ids
                };

                // Chuyển object thành chuỗi JSON
                var jsonResponse = JsonConvert.SerializeObject(response);

                return jsonResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Batch_Create_Item: {ex.Message}");
                throw;
            }
        }


        public async Task<List<int>> Batch_Create_Item_Subject(List<vm_teacher_class> requests)
        {
            try
            {
                var obj = _mapper.Map<List<Teacher_Class>>(requests);
                var teacherClassesToAdd = new List<Teacher_Class>();

                foreach (var teacherClass in obj)
                {
                    bool isSubjectExist = teacherClassesToAdd.Any(tc => tc.SubjectId == teacherClass.SubjectId);
                    if (!isSubjectExist) { teacherClassesToAdd.Add(teacherClass); }
                }

                // Thêm danh sách giáo viên không trùng môn học vào context
                _appContext.Teacher_Classes.AddRange(teacherClassesToAdd);
                await _appContext.SaveChangesAsync();

                var ids = teacherClassesToAdd.Select(t => t.Id).ToList() ?? new List<int>();
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
