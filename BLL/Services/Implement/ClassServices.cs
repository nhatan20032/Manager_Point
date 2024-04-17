using AutoMapper;
using AutoMapper.QueryableExtensions;
using BLL.Services.Interface;
using BLL.ViewModels;
using BLL.ViewModels.Class;
using BLL.ViewModels.Teacher_Class;
using BLL.ViewModels.User;
using Manager_Point.ApplicationDbContext;
using Manager_Point.Models;
using Manager_Point.Models.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;

namespace BLL.Services.Implement
{
    public class ClassServices : IClassServices
    {
        private readonly AppDbContext _appContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public ClassServices(IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _appContext = new AppDbContext();
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<List<int>> Batch_Create_Item(List<vm_create_class> requests)
        {
            try
            {
                var obj = _mapper.Map<List<Class>>(requests);
                _appContext.Classes.AddRange(obj);
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
                var classToDelete = await _appContext.Classes.Where(t => ids.Contains(t.Id)).ToListAsync();

                if (classToDelete.Any())
                {
                    _appContext.Classes.RemoveRange(classToDelete);
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

        public async Task<int> Create_Item(vm_create_class request)
        {
            try
            {
                var obj = _mapper.Map<Class>(request);
                _appContext.Classes.AddRange(obj);
                await _appContext.SaveChangesAsync();
                return obj.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Batch_Create_Item: {ex.Message}");
                throw;
            }
        }

        public async Task<string> Get_All_Async(int offset = 0, int limit = 10, string search = "")
        {
            try
            {
                int totalCount = _appContext.Classes
                      .Where(s => string.IsNullOrEmpty(search) || s.Name!.Contains(search))
                      .Count();

                int draw = 1;
                var httpRequest = _httpContextAccessor.HttpContext!.Request;
                if (httpRequest.Query.TryGetValue("draw", out StringValues valueDraw)) try { draw = int.Parse(valueDraw!); } catch { }

                var vm_class = _appContext.Classes.ProjectTo<vm_class>(_mapper.ConfigurationProvider).Where(t => string.IsNullOrEmpty(search) || t.Name!.Contains(search)).Skip(offset).Take(limit).ToList();
                var paginatedResult = new Pagination<vm_class>
                {
                    draw = draw,
                    recordsTotal = totalCount,
                    recordsFiltered = totalCount,
                    data = vm_class
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

        public List<vm_class> Get_List()
        {
            var vmClasses = _appContext.Classes
             .ProjectTo<vm_class>(_mapper.ConfigurationProvider)
             .ToList();

            return vmClasses;
        }

        public async Task<vm_class> Get_By_Id(int id)
        {
            try
            {
                var classes = _appContext.Classes.ProjectTo<vm_class>(_mapper.ConfigurationProvider).SingleOrDefault(x => x.Id == id);
                if (classes == null) return null!;
                return classes;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Get_By_Id: {ex.Message}");
                throw;
            }
        }

        public async Task<int> Modified_Item(int id, vm_update_class request)
        {
            try
            {
                var objForUpdate = await _appContext.Classes.FindAsync(id);
                if (objForUpdate == null) return -1;
                objForUpdate.CourseId = request.CourseId;
                objForUpdate.ClassCode = request.ClassCode;
                objForUpdate.GradeLevel = request.GradeLevel;
                objForUpdate.Name = request.Name;
                objForUpdate.Status = request.Status;
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
                var objToRemove = await _appContext.Classes.FindAsync(id);
                // Xử lý trường hợp không tìm thấy đối tượng
                if (objToRemove == null) return false;

                // Kiểm tra xem đối tượng đã được theo dõi trong DbContext hay không
                var local = _appContext.Classes.Local.FirstOrDefault(x => x.Id == id);

                // Sử dụng toán tử ba ngôi để xác định đối tượng cần xóa
                _appContext.Classes.Remove(local != null ? local : objToRemove);

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

        public async Task<string> GetClassOnBoard(int idUser)
        {
            var vm_class = await _appContext.Teacher_Classes
                .AsQueryable()
                .ProjectTo<vm_teacher_class_subject>(_mapper.ConfigurationProvider)
                .Where(x => x.UserId == idUser && x.TypeTeacher == TypeTeacher.Subject_Teacher)
                .ToListAsync();

            var user = await _appContext.Users
                .Include(u => u.User_Roles!).ThenInclude(ur => ur.Role!)
                .Include(u => u.Subject_Teachers!).ThenInclude(st => st.Subject)
                .Include(u => u.Teacher_Classes!).ThenInclude(tc => tc.Class)
                .AsQueryable()
                .ProjectTo<vm_teacher>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(x => x.Id == idUser);

            if (vm_class == null) { return null!; }
            // Group by ClassId and select one object from each group
            var resultList = vm_class
                .GroupBy(c => c.ClassId)
                .Select(group => new
                {
                    userData = user,
                    classData = _appContext.Classes.FirstOrDefault(x => x.Id == group.Key),
                    subjects = group.Select(g => _appContext.Subjects.FirstOrDefault(s => s.Id == g.SubjectId)).ToList(),
                    homeroom = _appContext.Users.FirstOrDefault(u =>
                        _appContext.Teacher_Classes.Any(vc => vc.ClassId == group.Key && vc.TypeTeacher == TypeTeacher.Homeroom_Teacher) && _appContext.Teacher_Classes.FirstOrDefault(vc => vc.ClassId == group.Key && vc.TypeTeacher == TypeTeacher.Homeroom_Teacher)!.UserId == u.Id)?.Name ?? "Chưa có",
                    status = _appContext.Classes.FirstOrDefault(s => s.Id == group.Key)!.Status == Status.During ? "Đang giảng dạy" : "Đã giảng dạy"
                })
                .ToList();

            var jsonResult = JsonConvert.SerializeObject(resultList, Formatting.Indented);
            return jsonResult;
        }
    }
}
