using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
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
using OfficeOpenXml;
using System.Globalization;
using Newtonsoft.Json;
using BLL.ViewModels.Teacher_Class;


namespace BLL.Services.Implement
{

    public class UserServices : IUserServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _appContext;
        private readonly IMapper _mapper;
        private readonly IJwtUtils _jwtUtils;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IClassServices _classServices;
        public UserServices(IClassServices classServices, IMapper mapper, IJwtUtils jwtUtils, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment)
        {
            _classServices = classServices;
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
                int draw = 1;
                var httpRequest = _httpContextAccessor.HttpContext!.Request;
                if (httpRequest.Query.TryGetValue("draw", out StringValues valueDraw)) try { draw = int.Parse(valueDraw!); } catch { }
                IQueryable<vm_user> vm_UserQuery = _appContext.Users
                .Include(u => u.User_Roles!).ThenInclude(ur => ur.Role!)
                .Include(u => u.Subject_Teachers!).ThenInclude(st => st.Subject)
                .Include(u => u.Teacher_Classes!).ThenInclude(tc => tc.Class)
                .Include(u => u.Student_Classes!).ThenInclude(tc => tc.Class).ThenInclude(c => c.Course)
                .AsQueryable()
                .ProjectTo<vm_user>(_mapper.ConfigurationProvider).Where(t => string.IsNullOrEmpty(search) || t.Name!.Contains(search));
                int totalCount = await vm_UserQuery.CountAsync();
                var final_result = await vm_UserQuery
                   .Skip(offset)
                   .Take(limit)
                   .ToListAsync();
                var paginatedResult = new Pagination<vm_user>
                {
                    draw = draw,
                    recordsTotal = totalCount,
                    recordsFiltered = totalCount,
                    data = final_result
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

        public async Task<string> Get_All_Teacher(int offset = 0, int limit = 10, string search = "", int subject = 0, int classes = 0, int check_subject = 0)
        {
            try
            {
                int draw = 1;
                var httpRequest = _httpContextAccessor.HttpContext!.Request;
                if (httpRequest.Query.TryGetValue("draw", out StringValues valueDraw)) try { draw = int.Parse(valueDraw!); } catch { }

                IQueryable<vm_teacher> vm_UserQuery = _appContext.Users
                .Include(u => u.User_Roles!).ThenInclude(ur => ur.Role!)
                .Include(u => u.Subject_Teachers!).ThenInclude(st => st.Subject)
                .Include(u => u.Teacher_Classes!).ThenInclude(tc => tc.Class)
                .AsQueryable()
                .ProjectTo<vm_teacher>(_mapper.ConfigurationProvider).Where(t => string.IsNullOrEmpty(search) || t.Name!.Contains(search))
                    .Where(t => t.Role_Code!.Any(t => t == "gv"));

                if (!string.IsNullOrEmpty(search))
                {
                    vm_UserQuery = vm_UserQuery.Where(t => t.Name!.Contains(search));
                }

                if (check_subject == 1)
                {
                    vm_UserQuery = vm_UserQuery.Where(t => t.Subject_User!.Any());
                }
                else if (check_subject == 2)
                {
                    vm_UserQuery = vm_UserQuery.Where(t => !t.Subject_User!.Any());
                }

                if (subject != 0)
                {
                    vm_UserQuery = vm_UserQuery.Where(t => t.Subject_id!.Contains(subject));
                }

                if (classes != 0)
                {
                    vm_UserQuery = vm_UserQuery.Where(t => t.Teacher_Class_id!.Contains(classes));
                }

                int totalCount = await vm_UserQuery.CountAsync();

                var final_result = await vm_UserQuery
                    .Skip(offset)
                    .Take(limit)
                    .ToListAsync();

                var paginatedResult = new Pagination<vm_teacher>
                {
                    draw = draw,
                    recordsTotal = totalCount,
                    recordsFiltered = totalCount,
                    data = final_result
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

        public async Task<string> Get_All_Teacher_No_HomeRoom(int offset = 0, int limit = 10, string search = "", int subject = 0, int classes = 0)
        {
            try
            {
                int draw = 1;
                var httpRequest = _httpContextAccessor.HttpContext!.Request;
                if (httpRequest.Query.TryGetValue("draw", out StringValues valueDraw)) try { draw = int.Parse(valueDraw!); } catch { }

                IQueryable<vm_teacher> vm_UserQuery = _appContext.Users
                .Include(u => u.User_Roles!).ThenInclude(ur => ur.Role!)
                .Include(u => u.Subject_Teachers!).ThenInclude(st => st.Subject)
                .Include(u => u.Teacher_Classes!).ThenInclude(tc => tc.Class)
                .AsQueryable()
                .ProjectTo<vm_teacher>(_mapper.ConfigurationProvider).Where(t => string.IsNullOrEmpty(search) || t.Name!.Contains(search))
                    .Where(t => t.Role_Code!.Any(t => t == "gv")).Where(t => !t.TypeTeacher!.Any(tt => tt == TypeTeacher.Homeroom_Teacher));

                if (!string.IsNullOrEmpty(search))
                {
                    vm_UserQuery = vm_UserQuery.Where(t => t.Name!.Contains(search));
                }

                if (subject != 0)
                {
                    vm_UserQuery = vm_UserQuery.Where(t => t.Subject_id!.Contains(subject));
                }

                if (classes != 0)
                {
                    vm_UserQuery = vm_UserQuery.Where(t => t.Teacher_Class_id!.Contains(classes));
                }

                int totalCount = await vm_UserQuery.CountAsync();

                var final_result = await vm_UserQuery
                    .Skip(offset)
                    .Take(limit)
                    .ToListAsync();

                var paginatedResult = new Pagination<vm_teacher>
                {
                    draw = draw,
                    recordsTotal = totalCount,
                    recordsFiltered = totalCount,
                    data = final_result
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


        public async Task<vm_teacher> Get_By_HomeRoom_Id(int idClass)
        {
            try
            {
                var vm_class = await _appContext.Teacher_Classes
                    .AsQueryable()
                    .ProjectTo<vm_teacher_class>(_mapper.ConfigurationProvider).SingleOrDefaultAsync(x => x.ClassId == idClass && x.TypeTeacher == TypeTeacher.Homeroom_Teacher);
                if (vm_class == null) { return null!; }
                var vm_User = await _appContext.Users
                    .Include(u => u.User_Roles!).ThenInclude(ur => ur.Role!)
                    .Include(u => u.Subject_Teachers!).ThenInclude(st => st.Subject)
                    .Include(u => u.Teacher_Classes!).ThenInclude(tc => tc.Class)
                    .AsQueryable()
                    .ProjectTo<vm_teacher>(_mapper.ConfigurationProvider).SingleOrDefaultAsync(x => x.Id == vm_class!.UserId);

                if (vm_User == null) return null!;
                return vm_User;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Get_By_Id: {ex.Message}");
                throw;
            }
        }
        public async Task<string> Get_By_Subject_Teacher_Id(int idClass)
        {
            try
            {
                var vm_class = await _appContext.Teacher_Classes
                    .AsQueryable()
                    .ProjectTo<vm_teacher_class_subject>(_mapper.ConfigurationProvider).Where(x => x.ClassId == idClass && x.TypeTeacher == TypeTeacher.Subject_Teacher).ToListAsync();
                if (vm_class == null) { return null!; }
                var resultList = vm_class.Select(classes => new
                {
                    data = _appContext.Users
                    .Include(u => u.User_Roles!).ThenInclude(ur => ur.Role!)
                    .Include(u => u.Subject_Teachers!).ThenInclude(st => st.Subject)
                    .Include(u => u.Teacher_Classes!).ThenInclude(tc => tc.Class)
                    .AsQueryable()
                    .ProjectTo<vm_teacher>(_mapper.ConfigurationProvider).SingleOrDefault(x => x.Id == classes.UserId),
                    subjectInClass = _appContext.Subjects.SingleOrDefault(x => x.Id == classes.SubjectId)!.Name,
                    idSubject = _appContext.Subjects.SingleOrDefault(x => x.Id == classes.SubjectId)!.Id,
                }).ToList();

                var jsonResult = JsonConvert.SerializeObject(resultList, Formatting.Indented);
                return jsonResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Get_By_Id: {ex.Message}");
                throw;
            }
        }

        public async Task<string> Count_All_Rank_Student()
        {
            try
            {
                var list_class = _classServices.Get_List();
                var list_id_class = list_class.Select(c => c.Id).ToList();
                List<StudentData> allStudents = new();

                foreach (var item in list_id_class)
                {
                    var students = await _classServices.GetRank(item);
                    allStudents.AddRange(students);
                }

                var rankCounts = allStudents
                    .GroupBy(student => student.Rank)
                    .Select(group => new
                    {
                        Rank = group.Key,
                        Student = group.Count()
                    })
                    .ToList();

                var jsonResult = JsonConvert.SerializeObject(rankCounts, Formatting.Indented);
                return jsonResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Count_All_Rank_Student: {ex.Message}");
                throw;
            }
        }

        public async Task<string> Count_All_Rank_Student_GradeLevel()
        {
            try
            {
                var list_class = _classServices.Get_List();
                var gradeLevels = list_class.Select(c => c.GradeLevel).Distinct().ToList();
                var allRanksByGradeLevel = new List<object>();

                foreach (var gradeLevel in gradeLevels)
                {
                    var list_id_class = list_class.Where(c => c.GradeLevel == gradeLevel).Select(c => c.Id).ToList();
                    List<StudentData> allStudents = new();

                    foreach (var item in list_id_class)
                    {
                        var students = await _classServices.GetRank(item);
                        allStudents.AddRange(students);
                    }

                    var rankCounts = allStudents
                        .GroupBy(student => student.Rank)
                        .Select(group => new
                        {
                            Rank = group.Key,
                            StudentCount = group.Count()
                        })
                        .ToList<object>(); // Convert to List<object>

                    var gradeLevelObject = new
                    {
                        GradeLevel = $"Khối {gradeLevel}",
                        Ranks = rankCounts
                    };

                    allRanksByGradeLevel.Add(gradeLevelObject);
                }

                var jsonResult = JsonConvert.SerializeObject(allRanksByGradeLevel, Formatting.Indented);
                return jsonResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetRanksByGradeLevel: {ex.Message}");
                throw;
            }
        }

        public async Task<string> Count_All_Rank_Student_Course()
        {
            try
            {
                var list_class = _classServices.Get_List();
                var list_id_class = list_class.Select(c => c.Id).ToList();

                var courses = await _appContext.Courses.ToListAsync();
                var filteredCourses = courses.Where(course => list_id_class.Contains(course.Id)).ToList();

                var courseYears = filteredCourses.Select(course => new
                {
                    course_id = course.Id,
                    course = $"Khoá {course.Name}"
                }).ToList();

                List<StudentData> allStudents = new();

                foreach (var item in list_id_class)
                {
                    var students = await _classServices.GetRank(item);
                    allStudents.AddRange(students);
                }

                var studentCourseYears = (from student in allStudents
                                          join coursese in courseYears on student.ClassId equals coursese.course_id
                                          select new
                                          {
                                              student.Rank,
                                              coursese.course
                                          }).ToList();

                var rankCountsByYear = studentCourseYears
                    .GroupBy(sc => new { sc.course, sc.Rank })
                    .Select(group => new
                    {
                        Course = group.Key.course,
                        Rank = group.Key.Rank,
                        Count = group.Count()
                    })
                    .GroupBy(r => r.Course)
                    .Select(group => new
                    {
                        Course = group.Key,
                        Ranks = group.Select(rankGroup => new
                        {
                            Rank = rankGroup.Rank,
                            Count = rankGroup.Count
                        }).ToList()
                    })
                    .ToList();

                var jsonResult = JsonConvert.SerializeObject(rankCountsByYear, Formatting.Indented);
                return jsonResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Count_All_Rank_Student_Year: {ex.Message}");
                throw;
            }
        }

        public async Task<string> Count_All_Rank_Student_Year()
        {
            try
            {
                // Step 1: Retrieve the list of classes
                var list_class = _classServices.Get_List();
                var list_id_class = list_class.Select(c => c.Id).ToList();

                // Step 2: Retrieve the courses and filter them based on the class IDs
                var courses = await _appContext.Courses.ToListAsync();
                var filteredCourses = courses.Where(course => list_id_class.Contains(course.Id)).ToList();

                // Step 3: Project the courses to include course_id and startYear
                var courseYears = filteredCourses.Select(course => new
                {
                    course_id = course.Id,
                    startYear = (course.EndTime?.Date.Year == course.StartTime?.Date.Year)
                                ? course.StartTime?.Date.Year.ToString()
                                : $"{course.StartTime?.Date.Year} - {course.EndTime?.Date.Year}"
                }).ToList();

                List<StudentData> allStudents = new();

                // Step 4: Get the students for each class and their ranks
                foreach (var item in list_id_class)
                {
                    var students = await _classServices.GetRank(item);
                    allStudents.AddRange(students);
                }

                // Step 5: Combine student data with course start year
                var studentCourseYears = (from student in allStudents
                                          join courseYear in courseYears on student.ClassId equals courseYear.course_id
                                          select new
                                          {
                                              student.Rank,
                                              courseYear.startYear
                                          }).ToList();

                // Step 6: Group by year and rank, then calculate the counts
                var rankCountsByYear = studentCourseYears
                    .GroupBy(sc => new { sc.startYear, sc.Rank })
                    .Select(group => new
                    {
                        Year = group.Key.startYear,
                        Rank = group.Key.Rank,
                        Count = group.Count()
                    })
                    .GroupBy(r => r.Year)
                    .Select(group => new
                    {
                        Year = group.Key,
                        Ranks = group.Select(rankGroup => new
                        {
                            Rank = rankGroup.Rank,
                            Count = rankGroup.Count
                        }).ToList()
                    })
                    .ToList();

                // Step 7: Serialize the results to JSON
                var jsonResult = JsonConvert.SerializeObject(rankCountsByYear, Formatting.Indented);
                return jsonResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Count_All_Rank_Student_Year: {ex.Message}");
                throw;
            }
        }



        public async Task<string> Count_All_Teacher_Student()
        {
            try
            {
                IQueryable<vm_user> vm_UserQuery = _appContext.Users
                    .Include(u => u.User_Roles!).ThenInclude(ur => ur.Role!)
                    .Include(u => u.Subject_Teachers!).ThenInclude(st => st.Subject)
                    .Include(u => u.Teacher_Classes!).ThenInclude(tc => tc.Class)
                    .AsQueryable()
                    .ProjectTo<vm_user>(_mapper.ConfigurationProvider);

                // Đếm số lượng giáo viên
                int teacherCount = await vm_UserQuery.CountAsync(t => t.Role_Code!.Any(t => t == "gv") && t.Status == Status.Active);

                // Đếm số lượng học sinh
                int studentCount = await vm_UserQuery.CountAsync(t => t.Role_Code!.Any(t => t == "hs") && t.Status == Status.Active);

                var result = new
                {
                    totalTeachers = teacherCount,
                    totalStudents = studentCount
                };

                var jsonResult = JsonConvert.SerializeObject(result, Formatting.Indented);
                return jsonResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Count_All_Teacher_Student: {ex.Message}");
                throw;
            }
        }


        public async Task<string> Count_Teachers_By_Subject()
        {
            try
            {
                var teacherCountsBySubject = new List<object>();

                var subjects = await _appContext.Subjects.ToListAsync();

                foreach (var subject in subjects)
                {
                    int totalCount = await _appContext.Users
                    .Include(u => u.User_Roles!).ThenInclude(ur => ur.Role!)
                    .Include(u => u.Subject_Teachers!).ThenInclude(st => st.Subject)
                    .Include(u => u.Teacher_Classes!).ThenInclude(tc => tc.Class)
                    .AsQueryable()
                    .ProjectTo<vm_teacher>(_mapper.ConfigurationProvider)
                    .Where(u => u.Role_Code!.Any(t => t == "gv") && u.Subject_id!.All(t => t == subject.Id) && u.Status == Status.Active)
                    .CountAsync();

                    var subjectInfo = new
                    {
                        SubjectName = subject.Name,
                        TeacherCount = totalCount
                    };

                    teacherCountsBySubject.Add(subjectInfo);
                }

                // Convert danh sách thành JSON
                var jsonResult = JsonConvert.SerializeObject(teacherCountsBySubject, Formatting.Indented);
                return jsonResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Count_Teachers_By_Subject: {ex.Message}");
                throw;
            }
        }


        public async Task<string> Get_All_Student(int offset = 0, int limit = 10, string search = "", int classes = 0, int check_class = 0)
        {
            try
            {
                int draw = 1;
                var httpRequest = _httpContextAccessor.HttpContext!.Request;
                if (httpRequest.Query.TryGetValue("draw", out StringValues valueDraw)) try { draw = int.Parse(valueDraw!); } catch { }
                IQueryable<vm_student> vm_UserQuery = _appContext.Users
                .AsNoTracking()
                .Include(u => u.User_Roles!).ThenInclude(ur => ur.Role!)
                .Include(u => u.Student_Classes!).ThenInclude(tc => tc.Class).ThenInclude(c => c.Course)
                .AsQueryable()
                .ProjectTo<vm_student>(_mapper.ConfigurationProvider).Where(t => string.IsNullOrEmpty(search) || t.Name!.Contains(search))
                .Where(u => u.Role_Code!.Any(rr => rr == "hs"));

                if (classes != 0) vm_UserQuery = vm_UserQuery.Where(t => t.Student_Class_id!.Any(t => t == classes));
                if (check_class == 1)
                {
                    vm_UserQuery = vm_UserQuery.Where(t => t.Student_Class_Code!.Count > 0);
                }
                else if (check_class == 2)
                {
                    vm_UserQuery = vm_UserQuery.Where(t => t.Student_Class_Code!.Count == 0);
                }
                int totalCount = await vm_UserQuery.CountAsync();
                var final_result = await vm_UserQuery
                    .Skip(offset)
                    .Take(limit)
                    .ToListAsync();
                var paginatedResult = new Pagination<vm_student>
                {
                    draw = draw,
                    recordsTotal = totalCount,
                    recordsFiltered = totalCount,
                    data = final_result
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
                IQueryable<vm_user> vm_UserQuery = _appContext.Users
                                .Include(u => u.User_Roles!).ThenInclude(ur => ur.Role!)
                                .Include(u => u.Student_Classes!).ThenInclude(tc => tc.Class)
                                .AsQueryable()
                                .ProjectTo<vm_user>(_mapper.ConfigurationProvider).Where(t => string.IsNullOrEmpty(search) || t.Name!.Contains(search))
                                    .Where(t => !t.Role_id!.Any());
                int draw = 1;
                var httpRequest = _httpContextAccessor.HttpContext!.Request;
                if (httpRequest.Query.TryGetValue("draw", out StringValues valueDraw)) try { draw = int.Parse(valueDraw!); } catch { }
                int totalCount = await vm_UserQuery.CountAsync();
                var final_result = await vm_UserQuery
                    .Skip(offset)
                    .Take(limit)
                    .ToListAsync();
                var paginatedResult = new Pagination<vm_user>
                {
                    draw = draw,
                    recordsTotal = totalCount,
                    recordsFiltered = totalCount,
                    data = final_result
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
                            User_Code = GenerateStudentCode(worksheet.Cells[row, 2].Value?.ToString(), usersToAdd),
                            Name = worksheet.Cells[row, 2].Value?.ToString(),
                            Gender = GenderSelection(worksheet.Cells[row, 3].Value?.ToString()),
                            Nation = NationSelection(worksheet.Cells[row, 4].Value?.ToString()),
                            Address = worksheet.Cells[row, 5].Value?.ToString(),
                            Email = worksheet.Cells[row, 6].Value?.ToString(),
                            PhoneNumber = worksheet.Cells[row, 7].Value?.ToString(),
                            Password = worksheet.Cells[row, 8].Value?.ToString() ?? "123456",
                            DOB = ConvertExcelDateToDateTime(worksheet.Cells[row, 9].Value?.ToString()),
                            Description = worksheet.Cells[row, 10].Value?.ToString(),
                            Status = StatusSelection(worksheet.Cells[row, 11].Value?.ToString())
                        };
                        usersToAdd.Add(user);
                    }

                    // Tạo danh sách User từ danh sách vm_create_user
                    var users = _mapper.Map<List<User>>(usersToAdd);

                    // Kiểm tra mã sinh viên có tồn tại không
                    var existingCodes = _appContext.Users
                        .Where(u => users.Select(newUser => newUser.User_Code).Contains(u.User_Code))
                        .Select(u => u.User_Code)
                        .ToList();

                    // Loại bỏ các User đã tồn tại trong danh sách mới
                    var newUsers = users.Where(u => !existingCodes.Contains(u.User_Code)).ToList();

                    // Thêm mới các User không trùng mã sinh viên
                    _appContext.Users.AddRange(newUsers);
                    await _appContext.SaveChangesAsync();

                    return newUsers.Count;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AddUsersFromExcel: {ex.Message}");
                throw;
            }
        }


        private string GenerateStudentCode(string? username, List<vm_create_user> usersToAdd)
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

            // Kiểm tra xem mã đã tồn tại trong danh sách usersToAdd chưa
            while (checkCode(code, usersToAdd))
            {
                randomNumber = random.Next(100);
                code = code.Substring(0, code.LastIndexOf('_') + 1) + randomNumber;
            }
            return code;
        }

        private bool checkCode(string code, List<vm_create_user> usersToAdd)
        {
            // Kiểm tra xem code có tồn tại trong danh sách usersToAdd không
            return usersToAdd.Any(u => u.User_Code == code);
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
                return baseDate.AddDays(doubleValue - 2);
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
        public async Task<string> Count_Student_In_Year()
        {
            try
            {
                var studentCourse = new List<object>();

                var courses = await _appContext.Courses.ToListAsync();

                foreach (var course in courses)
                {
                    int? startYear = course.StartTime?.Date.Year;
                    int? endYear = course.EndTime?.Date.Year;
                    int totalCount = await _appContext.Users
                                        .Include(u => u.User_Roles!).ThenInclude(ur => ur.Role!)
                                        .Include(u => u.Student_Classes!).ThenInclude(sc => sc.Class).ThenInclude(c => c.Course)
                                        .Where(u => u.Student_Classes!.Any(sc => sc.Class.Course.StartTime.HasValue &&
                                                                                sc.Class.Course.StartTime.Value.Year >= startYear &&
                                                                                sc.Class.Course.EndTime.HasValue &&
                                                                                sc.Class.Course.EndTime.Value.Year <= endYear))
                                        .AsQueryable()
                                        .ProjectTo<vm_student>(_mapper.ConfigurationProvider)
                                        .Where(u => u.Role_Code!.Contains("hs") && u.Course_id!.Contains(course.Id) && u.Status == Status.Active)
                                        .CountAsync();
                    if (startYear == endYear)
                    {
                        var CoursesInfo = new
                        {
                            Years = $"{startYear}",
                            Student = totalCount
                        };

                        studentCourse.Add(CoursesInfo);
                    }
                    else
                    {
                        var CoursesInfo = new
                        {
                            Years = $"{startYear} - {endYear}",
                            Student = totalCount
                        };

                        studentCourse.Add(CoursesInfo);
                    }
                }

                var jsonResult = JsonConvert.SerializeObject(studentCourse, Formatting.Indented);
                return jsonResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Count_Teachers_By_Subject: {ex.Message}");
                throw;
            }
        }
        public async Task<string> Count_Students_By_Class()
        {
            try
            {
                var studentCourse = new List<object>();

                var classes = await _appContext.Classes.ToListAsync();

                foreach (var classed in classes)
                {
                    int totalCount = await _appContext.Users
                    .Include(u => u.User_Roles!).ThenInclude(ur => ur.Role!)
                    .Include(u => u.Student_Classes!).ThenInclude(tc => tc.Class).ThenInclude(t => t.Course)
                    .AsQueryable()
                    .ProjectTo<vm_student>(_mapper.ConfigurationProvider)
                    .Where(u => u.Role_Code!.Contains("hs") && u.Student_Class_id!.Contains(classed.Id) && u.Status == Status.Active)
                    .CountAsync();
                    var classInfo = await _appContext.Classes
                        .Include(c => c.Course)  // Include the related Course entity
                        .Where(c => c.Id == classed.Id)
                        .Select(c => new
                        {
                            ClassCode = c.ClassCode,
                            CourseName = c.Course.Name // Assuming Course has a CourseName property
                        })
                        .FirstOrDefaultAsync();
                    var CoursesInfo = new
                    {
                        Classes = $"{classInfo!.ClassCode} - {classInfo!.CourseName}",
                        Student = totalCount
                    };

                    studentCourse.Add(CoursesInfo);
                }

                var jsonResult = JsonConvert.SerializeObject(studentCourse, Formatting.Indented);
                return jsonResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Count_Teachers_By_Subject: {ex.Message}");
                throw;
            }
        }
        public async Task<string> Count_Students_By_Course()
        {
            try
            {
                var studentCourse = new List<object>();

                var courses = await _appContext.Courses.ToListAsync();

                foreach (var course in courses)
                {
                    int totalCount = await _appContext.Users
                    .Include(u => u.User_Roles!).ThenInclude(ur => ur.Role!)
                    .Include(u => u.Student_Classes!).ThenInclude(tc => tc.Class).ThenInclude(t => t.Course)
                    .AsQueryable()
                    .ProjectTo<vm_student>(_mapper.ConfigurationProvider)
                    .Where(u => u.Role_Code!.Contains("hs") && u.Course_id!.Contains(course.Id) && u.Status == Status.Active)
                    .CountAsync();

                    var CoursesInfo = new
                    {
                        CourseName = course.Name,
                        Student = totalCount
                    };

                    studentCourse.Add(CoursesInfo);
                }

                var jsonResult = JsonConvert.SerializeObject(studentCourse, Formatting.Indented);
                return jsonResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Count_Teachers_By_Subject: {ex.Message}");
                throw;
            }
        }
    }
}
