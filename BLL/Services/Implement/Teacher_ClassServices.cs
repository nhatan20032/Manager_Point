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

        public Task<bool> Batch_Remove_Item(List<int> ids)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Batch_Remove_Item_HomeRoom(int idClass)
        {
            try
            {
                var class_teacher = _appContext.Teacher_Classes.Where(t => t.ClassId == idClass && t.TypeTeacher == TypeTeacher.Homeroom_Teacher).ToList();

                if (class_teacher.Any())
                {
                    _appContext.Teacher_Classes.RemoveRange(class_teacher);
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

        public Task<bool> Batch_Remove_Item_Subject(int UserId)

        {
            throw new NotImplementedException();
        }

        public Task<int> Create_Item(vm_teacher_class request)
        {
            throw new NotImplementedException();
        }

        public Task<int> Modified_Item(int id, vm_teacher_class request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove_Item(int id)
        {
            throw new NotImplementedException();
        }


        public Task<bool> Remove_Item_By_IdUser_and_IdSubject(int userId, int subjectId)
        {
            throw new NotImplementedException();
        }

    }
}