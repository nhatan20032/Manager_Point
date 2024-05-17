using AutoMapper;
using AutoMapper.QueryableExtensions;
using BLL.Services.Interface;
using BLL.ViewModels;
using BLL.ViewModels.GradePoint;
using Manager_Point.ApplicationDbContext;
using Manager_Point.Models;
using Manager_Point.Models.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Data.Entity;

namespace BLL.Services.Implement
{

    public class GradePointServices : IGradePointServices
	{
		private readonly AppDbContext _appContext;
		private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public GradePointServices(IHttpContextAccessor httpContextAccessor, IMapper mapper)
		{
			_httpContextAccessor = httpContextAccessor;
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
				var gradepointToDelete = _appContext.GradePoints.Where(t => ids.Contains(t.Id)).ToList();

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

		public async Task<float> TotalPoint(int id, int semester = 1)
		{
			var vm_gradepoint = _appContext.GradePoints.ProjectTo<vm_gradepoint>(_mapper.ConfigurationProvider).Where(c => c.UserId == id && c.Semester == (Semester)Enum.ToObject(typeof(Semester), semester)).ToList();
			float agvPoint = 0;

			foreach (var item in vm_gradepoint)
			{
				if (item.Average != null)
				{

					agvPoint += item.Average;
				}
			}
			return agvPoint;
		}

		public async Task<string> Get_All_Async(int id, int offset = 0, int limit = 10, string search = "", int semester = 1)
		{
			if (semester != 3)
			{
				try
				{


					var totalCount = _appContext.GradePoints
						.Where(t => string.IsNullOrEmpty(search) || t.Semester == (Semester)Enum.ToObject(typeof(Semester), semester)).Count();
					int draw = 1;
					var httpRequest = _httpContextAccessor.HttpContext!.Request;
					if (httpRequest.Query.TryGetValue("draw", out StringValues valueDraw)) try { draw = int.Parse(valueDraw!); } catch { }

					var vm_gradepoint = _appContext.GradePoints.ProjectTo<vm_gradepoint>(_mapper.ConfigurationProvider).Where(c => c.UserId == id && c.Semester == (Semester)Enum.ToObject(typeof(Semester), semester)).Skip(offset).Take(limit).ToList();


					foreach (var item in vm_gradepoint)
					{
						if (item.ExaminationPoint != null && item.ExaminationPoint.Any())
						{
							var objForUpdate = await _appContext.GradePoints.FindAsync(item.Id);
							float avg = (item.Midterm_Grades * 2 + item.Final_Grades * 3 + (item.ExaminationPoint).Sum() / (item.ExaminationPoint).Count()) / 6;
							objForUpdate.Average = avg;
							await _appContext.SaveChangesAsync();
						}
						else
						{
                            var objForUpdate = await _appContext.GradePoints.FindAsync(item.Id);
                            float avg = (item.Midterm_Grades * 2 + item.Final_Grades * 3 + 0) / 6;
                            objForUpdate.Average = avg;
                            await _appContext.SaveChangesAsync();
                        }
					}


					var paginatedResult = new Pagination<vm_gradepoint>
					{
						recordsTotal = totalCount,
						recordsFiltered = totalCount,
						draw = draw,
						data = vm_gradepoint
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
			return "";

		}

		public vm_gradepoint Get_By_Id(int ClassId, int UserId, int SubjectId, int Semester)
		{
			try
			{
				var gradepoint =  _appContext.GradePoints.ProjectTo<vm_gradepoint>(_mapper.ConfigurationProvider).FirstOrDefault(x => x.ClassId == ClassId && x.UserId == UserId && x.SubjectId == SubjectId && x.Semester == (Semester)Enum.ToObject(typeof(Semester), Semester));
				if (gradepoint == null) return null!;
				return gradepoint;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in Get_By_Id: {ex.Message}");
				throw;
			}
		}

		public async Task<int> ImportFromExcel(Stream excelFileStream)
		{
			int userId = 13;
			try
			{
				var gradeDataList = new List<vm_update_gradepoint>();
				var gradeDataListBug = new List<vm_update_gradepoint>();

                using (var package = new ExcelPackage(excelFileStream))
				{
					ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Lấy sheet đầu tiên

					int rowCount = worksheet.Dimension.Rows;
					int colCount = worksheet.Dimension.Columns;

					for (int row = 5; row <= rowCount; row++) // Bắt đầu từ hàng thứ hai, bỏ qua tiêu đề
					{
						var gradeData = new vm_update_gradepoint();
						var userIdByCode =  _appContext.Users.FirstOrDefault(c => c.User_Code == worksheet.Cells[row, 3].Value.ToString());
						var classIdByCode =  _appContext.Classes.FirstOrDefault(c => c.ClassCode == worksheet.Cells[row, 4].Value.ToString());
						var subjectById =  _appContext.Subjects.FirstOrDefault(c => c.Id == Convert.ToInt32(worksheet.Cells[row, 2].Value.ToString()));
						if (userIdByCode != null && classIdByCode != null && subjectById != null)
						{
							var teacherByIdSubject = _appContext.Subjects_Teachers.FirstOrDefault(c => c.SubjectId == Convert.ToInt32(worksheet.Cells[row, 2].Value.ToString()) && c.UserId == userId );
							if (teacherByIdSubject != null)
							{
								gradeData.SubjectId = Convert.ToInt32(worksheet.Cells[row, 2].Value.ToString());
								gradeData.UserId = userIdByCode.Id;
								gradeData.ClassId = classIdByCode.Id;
								gradeData.Semester = Enum.Parse<Semester>(worksheet.Cells[row, 5].Value.ToString());
								gradeData.Midterm_Grades = float.Parse(worksheet.Cells[row, 6].Value.ToString());
								gradeData.Final_Grades = float.Parse(worksheet.Cells[row, 7].Value.ToString());
								gradeDataList.Add(gradeData);
							}
						}
						
					}
					foreach (var gradeData in gradeDataList)
					{
                        #region cập nhật điểm
                       
                            var vm_gradepoint = _appContext.GradePoints.ProjectTo<vm_gradepoint>(_mapper.ConfigurationProvider).Where(c => c.UserId == gradeData.UserId && c.Semester == (Semester)Enum.ToObject(typeof(Semester), gradeData.Semester)).ToList();
                            foreach (var item in vm_gradepoint)
                            {
                                if (item.ExaminationPoint != null && item.ExaminationPoint.Any())
                                {
                                    var objForUpdate = await _appContext.GradePoints.FindAsync(item.Id);
                                    float avg = (item.Midterm_Grades * 2 + item.Final_Grades * 3 + (item.ExaminationPoint).Sum() / (item.ExaminationPoint).Count()) / 6;
                                    objForUpdate.Average = avg;
                                    await _appContext.SaveChangesAsync();
                                }
                                else
                                {
                                    var objForUpdate = await _appContext.GradePoints.FindAsync(item.Id);
                                    float avg = (item.Midterm_Grades * 2 + item.Final_Grades * 3 + 0) / 6;
                                    objForUpdate.Average = avg;
                                    await _appContext.SaveChangesAsync();
                                }
                            }
                        #endregion
                        var checkedGrade = _appContext.GradePoints.FirstOrDefault(c => c.ClassId == gradeData.ClassId && c.UserId == gradeData.UserId && c.SubjectId == gradeData.SubjectId);
						if (checkedGrade != null)
						{
							// Cập nhật thuộc tính chỉ khi nó là null hoặc 0
							if (checkedGrade.Midterm_Grades == null || checkedGrade.Midterm_Grades == 0)
								checkedGrade.Midterm_Grades = gradeData.Midterm_Grades;

							if (checkedGrade.Final_Grades == null || checkedGrade.Final_Grades == 0)
								checkedGrade.Final_Grades = gradeData.Final_Grades;

							// Cập nhật cơ sở dữ liệu
							_appContext.GradePoints.Update(checkedGrade);
						}
						else
						{
							// Thêm mới bản ghi nếu không tìm thấy
							var newGrade = _mapper.Map<GradePoint>(gradeData);
							_appContext.GradePoints.Add(newGrade);
						}
					}
					await _appContext.SaveChangesAsync();
					return gradeDataList.Count;

				}

			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in AddPointFromExcel: {ex.Message}");
				throw;
			}
		}
	/*	public async Task<(int, byte[])> ImportFromExcel(Stream excelFileStream)
		{
			try
			{
				var gradeDataList = new List<vm_update_gradepoint>();
				var invalidRows = new List<int>(); // Danh sách lưu trữ các hàng không hợp lệ

				using (var package = new ExcelPackage(excelFileStream))
				{
					ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Lấy sheet đầu tiên

					int rowCount = worksheet.Dimension.Rows;
					int colCount = worksheet.Dimension.Columns;

					for (int row = 2; row <= rowCount; row++) // Bắt đầu từ hàng thứ hai, bỏ qua tiêu đề
					{
						var gradeData = new vm_update_gradepoint();
						try
						{
							gradeData.SubjectId = Convert.ToInt32(worksheet.Cells[row, 1].Value.ToString());
							gradeData.UserId = Convert.ToInt32(worksheet.Cells[row, 2].Value.ToString());
							gradeData.ClassId = Convert.ToInt32(worksheet.Cells[row, 3].Value.ToString());
							gradeData.Semester = Enum.Parse<Semester>(worksheet.Cells[row, 4].Value.ToString());
							gradeData.Midterm_Grades = float.Parse(worksheet.Cells[row, 5].Value.ToString());
							gradeData.Final_Grades = float.Parse(worksheet.Cells[row, 6].Value.ToString());
							gradeData.Average = (gradeData.Midterm_Grades + gradeData.Final_Grades) / 2;
							gradeDataList.Add(gradeData);
						}
						catch (Exception ex)
						{
							// Nếu hàng không hợp lệ, thêm số thứ tự hàng vào danh sách không hợp lệ
							invalidRows.Add(row);
							Console.WriteLine($"Error in row {row}: {ex.Message}");
						}
					}

					// Kiểm tra xem có hàng không hợp lệ không
					if (invalidRows.Any())
					{
						// Tạo một tập tin Excel mới để chứa các hàng không hợp lệ
						using (var invalidPackage = new ExcelPackage())
						{
							ExcelWorksheet invalidWorksheet = invalidPackage.Workbook.Worksheets.Add("InvalidRows");

							// Sao chép các hàng không hợp lệ vào tập tin Excel mới
							foreach (var invalidRow in invalidRows)
							{
								for (int col = 1; col <= colCount; col++)
								{
									invalidWorksheet.Cells[invalidRow, col].Value = worksheet.Cells[invalidRow, col].Value;
								}
							}

							// Chuyển tập tin Excel thành mảng byte để trả về
							byte[] invalidExcelBytes = invalidPackage.GetAsByteArray();
							return (gradeDataList.Count, invalidExcelBytes);
						}
					}

					foreach (var gradeData in gradeDataList)
					{
						var checkedGrade = _appContext.GradePoints.FirstOrDefault(c => c.ClassId == gradeData.ClassId && c.UserId == gradeData.UserId && c.SubjectId == gradeData.SubjectId && c.Semester == gradeData.Semester);
						if (checkedGrade != null)
						{
							// Cập nhật thuộc tính chỉ khi nó là null hoặc 0
							if (checkedGrade.Midterm_Grades == null || checkedGrade.Midterm_Grades == 0)
								checkedGrade.Midterm_Grades = gradeData.Midterm_Grades;

							if (checkedGrade.Final_Grades == null || checkedGrade.Final_Grades == 0)
								checkedGrade.Final_Grades = gradeData.Final_Grades;

							// Cập nhật cơ sở dữ liệu
							_appContext.GradePoints.Update(checkedGrade);
						}
						else
						{
							// Thêm mới bản ghi nếu không tìm thấy
							var newGrade = _mapper.Map<GradePoint>(gradeData);
							_appContext.GradePoints.Add(newGrade);
						}
					}
					await _appContext.SaveChangesAsync();
					return (gradeDataList.Count, null); // Trả về null vì không có tập tin Excel không hợp lệ
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in AddUsersFromExcel: {ex.Message}");
				throw;
			}
		}
*/
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

		public async Task<string> GetSumPointWholeyear_Async(int id, int classid, string search = "")
		{
		
				var totalCount = _appContext.GradePoints
							 .Where(t => string.IsNullOrEmpty(search)).Count();
				int draw = 1;
				var httpRequest = _httpContextAccessor.HttpContext!.Request;
				if (httpRequest.Query.TryGetValue("draw", out StringValues valueDraw))
				{
					try
					{
						draw = int.Parse(valueDraw!);
					}
					catch
					{

					}
				}

				var vm_gradepoints = _appContext.GradePoints
					.Where(gp => gp.UserId == id && gp.ClassId == classid)
					.ToList();

				var results = new List<vm_gradepoint_whole_year>();
				var abc = vm_gradepoints.Select(gp => gp.SubjectId).Distinct();

				foreach (var subjectId in vm_gradepoints.Select(gp => gp.SubjectId).Distinct())
				{
					float k1 = 0;
					float k2 = 0;
					var gradesForSubject =  vm_gradepoints.Where(gp => gp.SubjectId == subjectId).ToList();
					foreach (var item in gradesForSubject)
					{
						if (item.Semester == (Semester)Enum.ToObject(typeof(Semester), 1))
						{
							k1 = item.Average;
						}
						if (item.Semester == (Semester)Enum.ToObject(typeof(Semester), 2))
						{
							k2 = item.Average;
						}

					}
				var result = new vm_gradepoint_whole_year
				{
						SubjectName = _appContext.Subjects.FirstOrDefault(c => c.Id == subjectId).Name,
						SubjectId = subjectId,
						Semester1 = k1,
						Semester2 = k2,
						Average_Whole_year = (k1 + k2*2)/3
					};

					results.Add(result);
				}

				var paginatedResult = new Pagination<vm_gradepoint_whole_year>
				{
					recordsTotal = totalCount,
					recordsFiltered = totalCount,
					draw = draw,
					data = results
				};

				var jsonResult = JsonConvert.SerializeObject(paginatedResult, Formatting.Indented);
				return jsonResult;
			
		}

		//public async Task<List<vm_class>> GetClassByUser(int userid)
		//{
		//	return 
		//}
	}

}
