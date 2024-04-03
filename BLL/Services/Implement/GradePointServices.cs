using AutoMapper;
using AutoMapper.QueryableExtensions;
using BLL.Services.Interface;
using BLL.ViewModels;
using BLL.ViewModels.GradePoint;
using Manager_Point.ApplicationDbContext;
using Manager_Point.Models;
using Manager_Point.Models.Enum;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Data.Entity;

namespace BLL.Services.Implement
{

    public class GradePointServices : IGradePointServices
    {
        private readonly AppDbContext _appContext;
        private readonly IMapper _mapper;
        public GradePointServices(IMapper mapper)
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

        public async Task<string> Get_All_Async(int page_number = 1, int page_size = 10, string search = "")
        {
            try
            {
                int skip = (page_number - 1) * page_size;
                var query = _appContext.GradePoints
                    .Where(t => string.IsNullOrEmpty(search))
                    .Skip(skip)
                    .Take(page_size);
                var subjects = query.ToList();

                int totalCount = _appContext.GradePoints
                    .Where(s => string.IsNullOrEmpty(search))
                    .Count();

                var vm_gradepoint = _appContext.GradePoints.ProjectTo<vm_gradepoint>(_mapper.ConfigurationProvider).ToList();
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
                var gradepoint = await _appContext.GradePoints.ProjectTo<vm_gradepoint>(_mapper.ConfigurationProvider).SingleOrDefaultAsync(x => x.Id == id);
                if (gradepoint == null) return null!;
                return gradepoint;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Get_By_Id: {ex.Message}");
                throw;
            }
        }

      /*  public async Task<int> ImportFromExcel(Stream excelFileStream)
        {

            try
            {
                var gradeDataList = new List<vm_update_gradepoint>();

                using (var package = new ExcelPackage(excelFileStream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Lấy sheet đầu tiên

                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    for (int row = 2; row <= rowCount; row++) // Bắt đầu từ hàng thứ hai, bỏ qua tiêu đề
                    {
                        var gradeData = new vm_update_gradepoint();
                        gradeData.SubjectId = Convert.ToInt32(worksheet.Cells[row, 1].Value.ToString());
                        gradeData.UserId = Convert.ToInt32(worksheet.Cells[row, 2].Value.ToString());
                        gradeData.ClassId = Convert.ToInt32(worksheet.Cells[row, 3].Value.ToString());
                        gradeData.Semester = Enum.Parse<Semester>(worksheet.Cells[row, 4].Value.ToString());
                        gradeData.Midterm_Grades = float.Parse(worksheet.Cells[row, 5].Value.ToString());
                        gradeData.Final_Grades = float.Parse(worksheet.Cells[row, 6].Value.ToString());
                        gradeData.Average = (gradeData.Midterm_Grades + gradeData.Final_Grades) / 2;
                        gradeDataList.Add(gradeData);
                    }
                    foreach (var gradeData in gradeDataList)
                    {
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
                Console.WriteLine($"Error in AddUsersFromExcel: {ex.Message}");
                throw;
            }
        }*/
		public async Task<(int, byte[])> ImportFromExcel(Stream excelFileStream)
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
					return (gradeDataList.Count, null); // Trả về null vì không có tập tin Excel không hợp lệ
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in AddUsersFromExcel: {ex.Message}");
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
