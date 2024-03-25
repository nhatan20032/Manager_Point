using AutoMapper;
using BLL.Services.Interface;
using BLL.ViewModels;
using BLL.ViewModels.Class;
using BLL.ViewModels.GradePoint;
using Manager_Point.ApplicationDbContext;
using Manager_Point.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Implement
{
	public class GradePointService : IGradePointService
	{
		private readonly AppDbContext _appContext;
		private readonly IMapper _mapper;
		public GradePointService(IMapper mapper)
        {
			_appContext = new AppDbContext();
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}
        public async Task<List<int>> Batch_Create_Item(List<vm_create_gradepoint> requests)
		{
			try
			{
				var obj = _mapper.Map<List<GradePoint>>(requests);
				_appContext.GradePoints.AddRange(obj);
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
				var gradepointToDelete = await _appContext.GradePoints.Where(t => ids.Contains(t.Id)).ToListAsync();

				if (gradepointToDelete.Any())
				{
					_appContext.GradePoints.RemoveRange(gradepointToDelete);
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

		public async Task<int> Create_Item(vm_create_gradepoint request)
		{
			try
			{
				var obj = _mapper.Map<GradePoint>(request);
				_appContext.GradePoints.AddRange(obj);
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
				var query = _appContext.GradePoints
					.Where(t => string.IsNullOrEmpty(search) )
					.Skip(skip)
					.Take(page_size);
				var subjects = query.ToList();

				int totalCount = _appContext.GradePoints
					.Where(s => string.IsNullOrEmpty(search) )
					.Count();

				var vm_gradepoint = _mapper.Map<List<vm_gradepoint>>(subjects);
				var paginatedResult = new PaginatedResult<vm_gradepoint>
				{
					TotalCount = totalCount,
					PageNumber = page_number,
					PageSize = page_size,
					Data = vm_gradepoint
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

		public async Task<vm_gradepoint> Get_By_Id(int id)
		{
			try
			{
				var existingSubject = await _appContext.GradePoints.FindAsync(id); // kiểm tra trog db context có không thì lấy luôn
				if (existingSubject != null)
				{
					var vm_gradepoint = _mapper.Map<vm_gradepoint>(existingSubject);
					return vm_gradepoint;
				}
				var gradepoint = await _appContext.GradePoints.FirstOrDefaultAsync(s => s.Id == id); // không thì truy cập vào db để lấy đối tượng ra
				if (gradepoint == null) return null!;
				var vm_gradepoint_fromDb = _mapper.Map<vm_gradepoint>(gradepoint);
				return vm_gradepoint_fromDb;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in Get_By_Id: {ex.Message}");
				throw;
			}
		}

		public async Task<int> Modified_Item(int id, vm_update_gradepoint request)
		{
			try
			{
				var objForUpdate = await _appContext.GradePoints.FindAsync(id);
				if (objForUpdate == null) return -1;
				objForUpdate.SubjectId = request.SubjectId;
				objForUpdate.UserId = request.UserId;
				objForUpdate.ClassId = request.ClassId;
				objForUpdate.Semester = request.Semester;
				objForUpdate.Midterm_Grades = request.Midterm_Grades;
				objForUpdate.Final_Grades = request.Final_Grades;
				objForUpdate.Average = request.Average;
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
				var objToRemove = await _appContext.GradePoints.FindAsync(id);
				// Xử lý trường hợp không tìm thấy đối tượng
				if (objToRemove == null) return false;

				// Kiểm tra xem đối tượng đã được theo dõi trong DbContext hay không
				var local = _appContext.GradePoints.Local.FirstOrDefault(x => x.Id == id);

				// Sử dụng toán tử ba ngôi để xác định đối tượng cần xóa
				_appContext.GradePoints.Remove(local != null ? local : objToRemove);

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
