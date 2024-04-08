using AutoMapper;
using AutoMapper.QueryableExtensions;
using BLL.Services.Interface;
using BLL.ViewModels;
using BLL.ViewModels.Course;
using BLL.ViewModels.Subject;
using Manager_Point.ApplicationDbContext;
using Manager_Point.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data.Entity;

namespace BLL.Services.Implement
{
	public interface ICourseServices
	{
		public Task<string> Get_All_Async(int offset = 0, int limit = 10, string search = "");
	//	public Task<string> Get_All_Name_course();
		public Task<vm_course> Get_By_Id(int id);
		public Task<int> Create_Item(vm_create_course request);
		public Task<List<int>> Batch_Create_Item(List<vm_create_course> requests);
		public Task<int> Modified_Item(int id, vm_update_course request);
		public Task<bool> Remove_Item(int id);
		public Task<bool> Batch_Remove_Item(List<int> ids);
	}
	public class CourseServices : ICourseServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _appContext;
        private readonly IMapper _mapper;
        public CourseServices(IHttpContextAccessor httpContextAccessor,IMapper mapper)
        {
			_httpContextAccessor = httpContextAccessor;
            _appContext = new AppDbContext();
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<List<int>> Batch_Create_Item(List<vm_create_course> requests)

		{
			try
			{
				var obj = _mapper.Map<List<Course>>(requests);
				_appContext.Courses.AddRange(obj);
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
				var courseToDelete = await _appContext.Courses.Where(t => ids.Contains(t.Id)).ToListAsync();

				if (courseToDelete.Any())
				{
					_appContext.Courses.RemoveRange(courseToDelete);
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

		public async Task<int> Create_Item(vm_create_course request)
		{
			try
			{
				var obj = _mapper.Map<Course>(request);
			//	obj.Name = request.StartTime.Value.Year.ToString() + "-" + request.EndTime.Value.Year.ToString();
				_appContext.Courses.Add(obj);
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

                int totalCount = _appContext.Courses
                    .Where(s => string.IsNullOrEmpty(search) || s.Name!.Contains(search))
                    .Count();

                int draw = 1;
                var httpRequest = _httpContextAccessor.HttpContext!.Request;
                if (httpRequest.Query.TryGetValue("draw", out StringValues valueDraw)) try { draw = int.Parse(valueDraw!); } catch { }

                var vm_courses = _appContext.Courses.ProjectTo<vm_course>(_mapper.ConfigurationProvider).Where(t => string.IsNullOrEmpty(search) || t.Name!.Contains(search)).Skip(offset).Take(limit).ToList();
                var paginatedResult = new Pagination<vm_course>
                {
                    draw = draw,
                    recordsTotal = totalCount,
                    recordsFiltered = totalCount,
                    data = vm_courses

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

	/*	public async Task<string> Get_All_Name_course()
		{
			try
			{
				int totalCount = _appContext.Courses.Count();
				var vm_courses = _appContext.Courses.ProjectTo<vm_course>(_mapper.ConfigurationProvider).ToList();
				int draw = 1;
				var httpRequest = _httpContextAccessor.HttpContext!.Request;
				if (httpRequest.Query.TryGetValue("draw", out StringValues valueDraw)) try { draw = int.Parse(valueDraw!); } catch { }
				var paginatedResult = new Pagination<vm_course>
				{
					draw = draw,
					recordsTotal = totalCount,
					recordsFiltered = totalCount,
					data = vm_courses

				};

				var jsonResult = JsonConvert.SerializeObject(paginatedResult, Formatting.Indented);
				return jsonResult;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in Get_All_Async: {ex.Message}");
				throw;
			}
		}*/

		public async Task<vm_course> Get_By_Id(int id)
		{
			try
			{

				var course =  _appContext.Courses.ProjectTo<vm_course>(_mapper.ConfigurationProvider).SingleOrDefault(x => x.Id == id);
				if (course == null) return null!;
				return course;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in Get_By_Id: {ex.Message}");
				throw;
			}
		}

		public async Task<int> Modified_Item(int id, vm_update_course request)
		{
			try
			{
				var objForUpdate = await _appContext.Courses.FindAsync(id);
				if (objForUpdate == null) return -1;
				objForUpdate.Name = request.Name;
				objForUpdate.StartTime = request.StartTime;
				objForUpdate.EndTime = request.EndTime;
				objForUpdate.Description = request.Description;
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
				var objToRemove = await _appContext.Courses.FindAsync(id);
				// Xử lý trường hợp không tìm thấy đối tượng
				if (objToRemove == null) return false;

				// Kiểm tra xem đối tượng đã được theo dõi trong DbContext hay không
				var local = _appContext.Courses.Local.FirstOrDefault(x => x.Id == id);

				// Sử dụng toán tử ba ngôi để xác định đối tượng cần xóa
				_appContext.Courses.Remove(local != null ? local : objToRemove);

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
