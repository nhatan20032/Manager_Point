using AutoMapper;
using AutoMapper.QueryableExtensions;
using BLL.Author;
using BLL.Authorization;
using BLL.Services.Interface;
using BLL.ViewModels;
using BLL.ViewModels.User;
using Manager_Point.ApplicationDbContext;
using Manager_Point.Models;
using Manager_Point.Models.Enum;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Data.Entity;
using System.Globalization;

namespace BLL.Services.Implement
{

	public class UserServices : IUserServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _appContext;
        private readonly IMapper _mapper;
        private readonly IJwtUtils _jwtUtils;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public UserServices(IMapper mapper, IJwtUtils jwtUtils, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment)
        {
            _appContext = new AppDbContext();
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _jwtUtils = jwtUtils;
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = webHostEnvironment;
        }
        public async Task<List<int>> Batch_Create_Item(List<vm_create_user> requests)
        {
            try
            {
                var obj = _mapper.Map<List<User>>(requests);
                _appContext.Users.AddRange(obj);
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
                var userDelete = _appContext.Users.Where(t => ids.Contains(t.Id)).ToList();

                if (userDelete.Any())
                {
                    _appContext.Users.RemoveRange(userDelete);
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

        public async Task<int> Create_Item(vm_create_user request)
        {
            try
            {
                var obj = _mapper.Map<User>(request);
                _appContext.Users.AddRange(obj);
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
                int totalCount = _appContext.Users.AsNoTracking()
                    .Where(s => string.IsNullOrEmpty(search) || s.Name!.Contains(search))
                    .Count();
                int draw = 1;
                var httpRequest = _httpContextAccessor.HttpContext!.Request;
                if (httpRequest.Query.TryGetValue("draw", out StringValues valueDraw)) try { draw = int.Parse(valueDraw!); } catch { }
                var vm_User = _appContext.Users.AsNoTracking().ProjectTo<vm_user>(_mapper.ConfigurationProvider);
                var result = vm_User.Where(t => (string.IsNullOrEmpty(search) || t.Name!.Contains(search))).Skip(offset).Take(limit).ToList();
                var paginatedResult = new Pagination<vm_user>
                {
                    draw = draw,
                    recordsTotal = totalCount,
                    recordsFiltered = totalCount,
                    data = result
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

        public async Task<string> Get_All_Teacher(int offset = 0, int limit = 10, string search = "", int subject = 0, int classes = 0)
        {
            try
            {
                int totalCount = _appContext.Users.AsNoTracking().ProjectTo<vm_user>(_mapper.ConfigurationProvider).Where(s => (string.IsNullOrEmpty(search) || s.Name!.Contains(search)) && s.Role_Code!.Contains("gv")).Count();
                int draw = 1;
                var httpRequest = _httpContextAccessor.HttpContext!.Request;
                if (httpRequest.Query.TryGetValue("draw", out StringValues valueDraw)) try { draw = int.Parse(valueDraw!); } catch { }
                var vm_User = _appContext.Users.AsNoTracking().ProjectTo<vm_user>(_mapper.ConfigurationProvider);
                var result = vm_User.Where(t => string.IsNullOrEmpty(search) || t.Name!.Contains(search)).Skip(offset).Take(limit).ToList();
                result = result.Where(t => t.Role_Code!.Contains("gv")).ToList();
                if (subject != 0 && classes == 0) result = result.Where(t => t.Subject_id!.Contains(subject)).ToList();
                if (classes != 0 && subject == 0) result = result.Where(t => t.Teacher_Class_id!.Contains(classes)).ToList();
                if (classes != 0 && subject != 0) result = result.Where(t => t.Teacher_Class_id!.Contains(classes) && t.Subject_id!.Contains(subject)).ToList();
                var paginatedResult = new Pagination<vm_user>
                {
                    draw = draw,
                    recordsTotal = totalCount,
                    recordsFiltered = totalCount,
                    data = result
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

        public async Task<string> Get_All_Student(int offset = 0, int limit = 10, string search = "", int classes = 0)
        {
            try
            {
                int totalCount = _appContext.Users.AsNoTracking().ProjectTo<vm_user>(_mapper.ConfigurationProvider).Where(s => (string.IsNullOrEmpty(search) || s.Name!.Contains(search)) && s.Role_Code!.Contains("hs")).Count();
                int draw = 1;
                var httpRequest = _httpContextAccessor.HttpContext!.Request;
                if (httpRequest.Query.TryGetValue("draw", out StringValues valueDraw)) try { draw = int.Parse(valueDraw!); } catch { }
                var vm_User = _appContext.Users.AsNoTracking().ProjectTo<vm_user>(_mapper.ConfigurationProvider);
                var result = vm_User.Where(t => string.IsNullOrEmpty(search) || t.Name!.Contains(search)).Skip(offset).Take(limit).ToList();
                result = result.Where(t => t.Role_Code!.Contains("hs")).ToList();
                if (classes != 0) result = result.Where(t => t.Student_Class_id!.Contains(classes)).ToList();
                var paginatedResult = new Pagination<vm_user>
                {
                    draw = draw,
                    recordsTotal = totalCount,
                    recordsFiltered = totalCount,
                    data = result
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
        public async Task<string> Get_User_No_Role(int offset = 0, int limit = 10, string search = "")
        {
            try
            {
                int totalCount = _appContext.Users.AsNoTracking().ProjectTo<vm_user>(_mapper.ConfigurationProvider).Where(s => (string.IsNullOrEmpty(search) || s.Name!.Contains(search)) && !s.Role_id!.Any()).Count();
                int draw = 1;
                var httpRequest = _httpContextAccessor.HttpContext!.Request;
                if (httpRequest.Query.TryGetValue("draw", out StringValues valueDraw)) try { draw = int.Parse(valueDraw!); } catch { }
                var vm_User = _appContext.Users.AsNoTracking().ProjectTo<vm_user>(_mapper.ConfigurationProvider);
                var result = vm_User.Where(t => (string.IsNullOrEmpty(search) || t.Name!.Contains(search)) && !t.Role_id!.Any()).Skip(offset).Take(limit).ToList();
                var paginatedResult = new Pagination<vm_user>
                {
                    draw = draw,
                    recordsTotal = totalCount,
                    recordsFiltered = totalCount,
                    data = result
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

        public async Task<vm_user> Get_By_Id(int id)
        {
            try
            {
                var vm_User = _appContext.Users.ProjectTo<vm_user>(_mapper.ConfigurationProvider).SingleOrDefault(x => x.Id == id);
                if (vm_User == null) return null!;
                return vm_User;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Get_By_Id: {ex.Message}");
                throw;
            }
        }

        public async Task<int> Modified_Item(int id, vm_update_user request)
        {
            try
            {
                var objForUpdate = await _appContext.Users.FindAsync(id);
                if (objForUpdate == null) return -1;
                objForUpdate.AvatarUrl = request.AvatarUrl;
                objForUpdate.PhoneNumber = request.PhoneNumber;
                objForUpdate.Name = request.Name;
                objForUpdate.Description = request.Description;
                objForUpdate.DOB = request.DOB;
                objForUpdate.Gender = request.Gender;
                objForUpdate.Address = request.Address;
                objForUpdate.Nation = request.Nation;
                objForUpdate.Status = request.Status;
                objForUpdate.Email = request.Email;
                // Không cần gọi Attach hoặc Update vì objForUpdate đã được theo dõi trong DbContext neuse có sự thay đổi thì nó sẽ cập nhật vô DB
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
                var objToRemove = await _appContext.Users.FindAsync(id);
                // Xử lý trường hợp không tìm thấy đối tượng
                if (objToRemove == null) return false;

                // Kiểm tra xem đối tượng đã được theo dõi trong DbContext hay không
                var local = _appContext.Users.Local.FirstOrDefault(x => x.Id == id);

                // Sử dụng toán tử ba ngôi để xác định đối tượng cần xóa
                _appContext.Users.Remove(local != null ? local : objToRemove);

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
        public async Task<int> AddUsersFromExcel(Stream excelFileStream)
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(excelFileStream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Chọn sheet đầu tiên
                    int rowCount = worksheet.Dimension.Rows;

                    List<vm_create_user> usersToAdd = new();

                    for (int row = 2; row <= rowCount; row++) // Bắt đầu từ dòng thứ 2, vì dòng đầu tiên là header
                    {
                        var user = new vm_create_user
                        {
                            User_Code = GenerateStudentCode(worksheet.Cells[row, 2].Value.ToString() ?? "null"),
                            Name = worksheet.Cells[row, 2].Value.ToString(),
                            Gender = GenderSelection((worksheet.Cells[row, 3].Value.ToString() ?? "1")),
                            Nation = NationSelection(worksheet.Cells[row, 4].Value.ToString()),
                            Address = worksheet.Cells[row, 5].Value.ToString(),
                            Email = worksheet.Cells[row, 6].Value.ToString(),
                            PhoneNumber = worksheet.Cells[row, 7].Value.ToString(),
                            Password = worksheet.Cells[row, 8].Value.ToString() ?? "123456",
                            DOB = ConvertExcelDateToDateTime(worksheet.Cells[row, 9].Value.ToString()),
                            Description = worksheet.Cells[row, 10].Value.ToString(),
                            Status = StatusSelection(worksheet.Cells[row, 11].Value.ToString() ?? "1")
                        };
                        usersToAdd.Add(user);
                    }
                    var obj = _mapper.Map<List<User>>(usersToAdd);
                    _appContext.Users.AddRange(obj);
                    await _appContext.SaveChangesAsync();
                    return usersToAdd.Count;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AddUsersFromExcel: {ex.Message}");
                throw;
            }
        }
        private string GenerateStudentCode(string? username)
        {
            string[] words = username!.Trim().Split(' ');
            string code = "US_";

            foreach (var word in words)
            {
                if (!string.IsNullOrWhiteSpace(word))
                {
                    char firstLetter = char.ToUpper(word[0]);
                    code += firstLetter;
                }
            }

            Random random = new Random();
            int randomNumber = random.Next(100);
            code += "_" + randomNumber;
            while (checkCode(code))
            {
                randomNumber = random.Next(100);
                code = code.Substring(0, code.LastIndexOf('_') + 1) + randomNumber;
            }
            return code;
        }
        private bool checkCode(string code)
        {
            var vm_User = _appContext.Users.ProjectTo<vm_user>(_mapper.ConfigurationProvider).SingleOrDefault(x => x.User_Code!.Contains(code));
            if (vm_User == null) return false;
            return true;
        }
        private Status StatusSelection(string? status)
        {
            return status switch
            {
                "Hoạt động" => Status.Active,
                "Thất bại" => Status.Failed,
                "Ra trường" => Status.Pass,
                "Kết thúc" => Status.Ended,
                "Đang xử lý" => Status.During,
                _ => Status.Active,
            };
        }
        private Gender GenderSelection(string? gender)
        {
            return gender switch
            {
                "Nam" => Gender.Male,
                "Nữ" => Gender.Female,
                "Khác" => Gender.Other,
                _ => Gender.Other,
            };
        }
        private string NationSelection(string? nation)
        {
            return nation switch
            {
                "Việt Nam" => "vi",
                "Trung Quốc" => "zh",
                "Hàn Quốc" => "kr",
                "Mỹ" => "us",
                "Anh" => "uk",
                _ => "vi",
            };
        }
        private DateTime ConvertExcelDateToDateTime(string? excelDate)
        {
            double.TryParse(excelDate, out double doubleValue);
            if (DateTime.TryParseExact(excelDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
            {
                return result;
            }
            else if (DateTime.TryParseExact(excelDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return result;
            }
            else if (DateTime.TryParseExact(excelDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return result;
            }
            else
            {
                DateTime baseDate = new DateTime(1900, 1, 1);
                return baseDate.AddDays(doubleValue - 2); // Excel date is 1-based, with 1 being 1900-01-01
            }
        }


        public AuthenticateResponse? Authenticate(AuthenticateRequest model)
        {
            var vm_User = _appContext.Users.ProjectTo<vm_user>(_mapper.ConfigurationProvider).Where(x => x.User_Code == model.UserCode && x.Password == model.Password).SingleOrDefault();
            // return null if user not found
            if (vm_User == null) return null;

            // authentication successful so generate jwt token
            var token = _jwtUtils.GenerateJwtToken(vm_User);

            return new AuthenticateResponse(vm_User, token);
        }       
    }
}
