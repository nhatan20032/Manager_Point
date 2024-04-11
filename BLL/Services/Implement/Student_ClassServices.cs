using AutoMapper;
using BLL.Services.Interface;
using BLL.ViewModels.GradePoint;
using BLL.ViewModels.Student_Class;
using Manager_Point.ApplicationDbContext;
using Manager_Point.Models;
using Manager_Point.Models.Enum;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services.Implement
{

    public class Student_ClassServices : IStudent_ClassServices
    {
        private readonly AppDbContext _appContext;
        private readonly IMapper _mapper;
        public Student_ClassServices(IMapper mapper)
        {
            _appContext = new AppDbContext();
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<List<int>> Batch_Create_Item(List<vm_student_class> requests)
        {
            try
            {
                var obj = _mapper.Map<List<Student_Class>>(requests);

                var classIds = obj.Select(sc => sc.ClassId).Distinct().ToList();
                var classCounts = await _appContext.Students_Classes
                    .Where(sc => classIds.Contains(sc.ClassId))
                    .GroupBy(sc => sc.ClassId)
                    .Select(g => new { ClassId = g.Key, Count = g.Count() })
                    .ToDictionaryAsync(x => x.ClassId, x => x.Count);
                var invalidClassIds = classIds.Where(id => classCounts.ContainsKey(id) && classCounts[id] >= 45).ToList();
                if (invalidClassIds.Any())
                {
                    throw new Exception($"ClassIds {string.Join(", ", invalidClassIds)} already have 45 or more records. Cannot add more.");
                }
                _appContext.Students_Classes.AddRange(obj);
                await _appContext.SaveChangesAsync();

                var userId = obj.Select(sc => sc.UserId).Distinct().ToList();
                var subjectIds = await _appContext.Subjects.Select(t => t.Id).ToListAsync();

                var gradePoints = CreateGradePoints(userId, subjectIds, classIds);
                var mappedGradePoints = _mapper.Map<List<GradePoint>>(gradePoints);
                _appContext.GradePoints.AddRange(mappedGradePoints);
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
        public List<vm_create_gradepoint> CreateGradePoints(List<int> userIds, List<int> subjectIds, List<int> classIds)
        {
            var gradePoints = userIds
                .SelectMany(userId =>
                    subjectIds.SelectMany(subjectId => new[]
                    {
                new vm_create_gradepoint
                {
                    UserId = userId,
                    SubjectId = subjectId,
                    ClassId = classIds.First(),
                    Semester = Semester.First_Semester,
                    Midterm_Grades = 0,
                    Final_Grades = 0,
                    Average = 0
                },
                new vm_create_gradepoint
                {
                    UserId = userId,
                    SubjectId = subjectId,
                    ClassId = classIds.First(),
                    Semester = Semester.Second_Semester,
                    Midterm_Grades = 0,
                    Final_Grades = 0,
                    Average = 0
                }
                    }))
                .ToList();

            return gradePoints;
        }



        public async Task<bool> Batch_Remove_Item(List<int> ids)
        {
            try
            {
                var student_Classes = _appContext.Students_Classes.Where(t => ids.Contains(t.Id)).ToList();

                if (student_Classes.Any())
                {
                    _appContext.Students_Classes.RemoveRange(student_Classes);
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
                var class_user = _appContext.Students_Classes.Where(t => t.UserId == userIds).ToList();

                if (class_user.Any())
                {
                    _appContext.Students_Classes.RemoveRange(class_user);
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

        public async Task<int> Create_Item(vm_student_class request)
        {
            try
            {
                var obj = _mapper.Map<Student_Class>(request);
                _appContext.Students_Classes.AddRange(obj);
                await _appContext.SaveChangesAsync();
                return obj.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Batch_Create_Item: {ex.Message}");
                throw;
            }
        }

        public async Task<int> Modified_Item(int id, vm_student_class request)
        {
            try
            {
                var objForUpdate = await _appContext.Students_Classes.FindAsync(id);
                if (objForUpdate == null) return -1;
                objForUpdate.ClassId = request.ClassId;
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
                var objToRemove = await _appContext.Students_Classes.FindAsync(id);
                // Xử lý trường hợp không tìm thấy đối tượng
                if (objToRemove == null) return false;

                // Kiểm tra xem đối tượng đã được theo dõi trong DbContext hay không
                var local = _appContext.Students_Classes.Local.FirstOrDefault(x => x.Id == id);

                // Sử dụng toán tử ba ngôi để xác định đối tượng cần xóa
                _appContext.Students_Classes.Remove(local != null ? local : objToRemove);

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
