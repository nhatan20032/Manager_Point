using AutoMapper;
using AutoMapper.QueryableExtensions;
using BLL.Services.Interface;
using BLL.ViewModels;
using BLL.ViewModels.Class;
using BLL.ViewModels.GradePoint;
using BLL.ViewModels.Teacher_Class;
using BLL.ViewModels.User;
using Manager_Point.ApplicationDbContext;
using Manager_Point.Models;
using Manager_Point.Models.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace BLL.Services.Implement
{
    public class SubjectByClass
    {
        public int Semester { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public float Avegare
        {
            get => (float)Math.Round(_average, 1);
            set => _average = value;
        }

        private float _average;
        //public string Rank { get; set; }
    }
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
        public async Task<string> Get_By_Id(int id)
        {
            try
            {
                var classes = _appContext.Classes.ProjectTo<vm_class>(_mapper.ConfigurationProvider).SingleOrDefault(x => x.Id == id);
                if (classes == null) return null!;
                var jsonResult = JsonConvert.SerializeObject(classes, Formatting.Indented);
                return jsonResult;
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

            if (vm_class == null || user == null) { return null!; }
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
        public async Task<string> GetHomeRoomOnBoard(int idUser)
        {
            var vm_class = await _appContext.Teacher_Classes
                .AsQueryable()
                .ProjectTo<vm_teacher_class_subject>(_mapper.ConfigurationProvider)
                .Where(x => x.UserId == idUser && x.TypeTeacher == TypeTeacher.Homeroom_Teacher)
                .ToListAsync();

            var user = await _appContext.Users
                .Include(u => u.User_Roles!).ThenInclude(ur => ur.Role!)
                .Include(u => u.Subject_Teachers!).ThenInclude(st => st.Subject)
                .Include(u => u.Teacher_Classes!).ThenInclude(tc => tc.Class)
                .AsQueryable()
                .ProjectTo<vm_teacher>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(x => x.Id == idUser);

            if (vm_class == null || user == null) { return null!; }
            var resultList = vm_class
                .GroupBy(c => c.ClassId)
                .Select(group => new
                {
                    userData = user,
                    classData = _appContext.Classes.FirstOrDefault(x => x.Id == group.Key),
                    status = _appContext.Classes.FirstOrDefault(s => s.Id == group.Key)!.Status == Status.During ? "Đang chủ nhiệm" : "Đã chủ nhiệm"
                })
                .ToList();

            var jsonResult = JsonConvert.SerializeObject(resultList, Formatting.Indented);
            return jsonResult;
        }
        public async Task<string> GradePointByClass(int idClass, int? semester)
        {
            var query = _appContext.GradePoints.Where(x => x.ClassId == idClass);

            if (semester.HasValue)
            {
                query = query.Where(x => x.Semester == (Semester)Enum.ToObject(typeof(Semester), semester));
            }

            var classGradePoints = query.ToList();
            var groupData = new List<StudentData>();
            var totalGroupData = groupData.Count();

            foreach (var gradePoint in classGradePoints)
            {
                var user = _appContext.Users.FirstOrDefault(u => u.Id == gradePoint.UserId);
                if (user == null)
                    continue;
                // cập nhật điểm 
                #region cập nhật điểm
                if (semester == 1 || semester == 2)
                {
                    var vm_gradepoint = _appContext.GradePoints.ProjectTo<vm_gradepoint>(_mapper.ConfigurationProvider).Where(c => c.UserId == user.Id && c.Semester == (Semester)Enum.ToObject(typeof(Semester), semester)).ToList();


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
                }
                #endregion
                var existingUserData = groupData.FirstOrDefault(d => d.UserId == gradePoint.UserId);
                if (existingUserData != null)
                {
                    // Kiểm tra xem môn học đã tồn tại trong danh sách chưa
                    var existingSubject = existingUserData.SubjectClasses.FirstOrDefault(s => s.SubjectId == gradePoint.SubjectId);

                    if (existingSubject != null)
                    {

                        // Môn học đã tồn tại, cập nhật trung bình
                        existingSubject.Avegare = (existingSubject.Avegare + gradePoint.Average) / 2;
                        // existingSubject.Rank = GetRank(gradePoint.Average);
                        // existingUserData.TotalPoint  += gradePoint.Average;

                    }
                    else
                    {
                        // Môn học chưa tồn tại, thêm mới vào danh sách
                        existingUserData.SubjectClasses.Add(new SubjectByClass
                        {
                            SubjectId = gradePoint.SubjectId,
                            SubjectName = _appContext.Subjects.FirstOrDefault(s => s.Id == gradePoint.SubjectId)?.Name,
                            Avegare = gradePoint.Average,
                            //  Rank = GetRank(gradePoint.Average),


                        });
                        existingUserData.TotalPoint += gradePoint.Average;
                    }

                }
                else
                {
                    // Người dùng chưa tồn tại trong groupData, tạo một mục mới
                    var userData = new StudentData
                    {
                        ClassId = gradePoint.ClassId,
                        ClassName = _appContext.Classes.FirstOrDefault(c => c.Id == gradePoint.ClassId)?.Name,
                        UserName = user.Name,
                        UserId = gradePoint.UserId,
                        SubjectClasses = new List<SubjectByClass>
                        {
                            new SubjectByClass
                            {
                                SubjectId = gradePoint.SubjectId,
                                SubjectName = _appContext.Subjects.FirstOrDefault(s => s.Id == gradePoint.SubjectId)?.Name,
                                Avegare = gradePoint.Average
                            }
                        },


                    };
                    userData.TotalPoint = gradePoint.Average;

                    groupData.Add(userData);
                }

            }
            foreach (var userData in groupData)
            {
                userData.TotalPoint /= userData.SubjectClasses.Count();
                userData.Rank = GetRankCN(userData.TotalPoint, userData.SubjectClasses);
            };


            var paginatedResult = new Pagination<StudentData>
            {
                // Có thể cần cập nhật draw tại đây
                draw = totalGroupData,
                recordsFiltered = totalGroupData,
                recordsTotal = totalGroupData,
                data = groupData
            };

            var jsonResult = JsonConvert.SerializeObject(paginatedResult, Formatting.Indented);
            return jsonResult;
        }
        private string GetRank(float average)
        {
            if (average < 5)
            {
                return "yeu";
            }
            else if (average < 6.5)
            {
                return "trung binh";
            }
            else if (average < 8)
            {
                return "kha";
            }
            else if (average <= 10)
            {
                return "gioi";
            }
            else
            {
                return "Kém";
            }
        }
        private string GetRankCN(float average, List<SubjectByClass> subjectByClasses)
        {
            int gioi = 0;
            int kha = 0;
            int trungbinh = 0;
            int yeu = 0;
            int kem = 0;
            float toan = 0;
            float van = 0;

            foreach (var item in subjectByClasses)
            {
                if (item.SubjectName.Contains("Toán"))
                {
                    toan = item.Avegare;
                }
                if (item.SubjectName.Contains("Văn"))
                {
                    van = item.Avegare;
                }

                if (item.Avegare < 5 && item.Avegare > 3.5)
                {
                    yeu++;
                }
                else if (item.Avegare < 6.5 && (toan >= 5 || van >= 5))
                {
                    trungbinh++;
                }
                else if (item.Avegare < 8 && (toan >= 6.5 || van >= 6.5))
                {
                    kha++;
                }
                else if (item.Avegare <= 10 && (toan >= 8 || van >= 8))
                {
                    gioi++;
                }
                else
                {
                    kem++;
                }
            }

            if (yeu > 0)
            {
                return "Yếu";
            }
            else if (average < 6.5 && trungbinh > 0)
            {
                return "Trung bình";
            }
            else if (average < 8 && kha > 0)
            {
                return "Khá";
            }
            else if (average <= 10 && gioi > 0)
            {
                return "Giỏi";
            }
            else
            {
                return "Kém";
            }
        }

        public async Task<vm_class> Get_By_Id_vm_class(int id)
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
        public async Task<string> GradePointSubjectByClass(int idClass, int idUser, int? semester = null)
        {
            var teacherSubject = _appContext.Teacher_Classes.FirstOrDefault(c => c.ClassId == idClass && c.UserId == idUser);
            if (semester == 3)
            {
                var results = new List<vm_gradepoint_whole_year>();
                var vm_gradePoint = _appContext.GradePoints.ProjectTo<vm_gradepoint>(_mapper.ConfigurationProvider).Where(c => c.SubjectId == teacherSubject.SubjectId && c.ClassId == idClass).ToList();
                var userGradePoints = new Dictionary<int, Dictionary<int, (float k1, float k2)>>();

                foreach (var gradePoint in vm_gradePoint)
                {
                    if (!userGradePoints.ContainsKey(gradePoint.UserId))
                    {
                        userGradePoints[gradePoint.UserId] = new Dictionary<int, (float k1, float k2)>();
                    }

                    if (!userGradePoints[gradePoint.UserId].ContainsKey(gradePoint.SubjectId))
                    {
                        userGradePoints[gradePoint.UserId][gradePoint.SubjectId] = (0, 0);
                    }

                    if (gradePoint.Semester == Semester.First_Semester)
                    {
                        userGradePoints[gradePoint.UserId][gradePoint.SubjectId] = (gradePoint.Average, userGradePoints[gradePoint.UserId][gradePoint.SubjectId].k2);
                    }
                    else if (gradePoint.Semester == Semester.Second_Semester)
                    {
                        userGradePoints[gradePoint.UserId][gradePoint.SubjectId] = (userGradePoints[gradePoint.UserId][gradePoint.SubjectId].k1, gradePoint.Average);
                    }
                }

                foreach (var userId in userGradePoints.Keys)
                {
                    foreach (var subjectId in userGradePoints[userId].Keys)
                    {
                        var grades = userGradePoints[userId][subjectId];
                        var result = new vm_gradepoint_whole_year
                        {
                            SubjectId = subjectId,
                            UserName = vm_gradePoint.First(gp => gp.UserId == userId).userName, // Lấy tên người dùng từ bất kỳ bản ghi nào của người dùng
                            SubjectName = _appContext.Subjects.FirstOrDefault(c => c.Id == subjectId)?.Name,
                            Semester1 = grades.k1,
                            Semester2 = grades.k2,
                            Average_Whole_year = (grades.k1 + grades.k2 * 2) / 3
                        };

                        results.Add(result);
                    }
                }

                var paginatedResult = new Pagination<vm_gradepoint_whole_year>
                {
                    draw = 1,
                    recordsTotal = results.Count,
                    recordsFiltered = results.Count,
                    data = results
                };
                var jsonResult = JsonConvert.SerializeObject(paginatedResult, Formatting.Indented);
                return jsonResult;
            }
            var vm_gradePoint1 = _appContext.GradePoints.ProjectTo<vm_gradepoint>(_mapper.ConfigurationProvider).Where(c => c.SubjectId == teacherSubject.SubjectId && c.ClassId == idClass && c.Semester == (Semester)Enum.ToObject(typeof(Semester), semester)).ToList();
            var paginatedResult1 = new Pagination<vm_gradepoint>
            {
                draw = 1,
                recordsTotal = 1,
                recordsFiltered = 1,
                data = vm_gradePoint1
            };
            var jsonResult1 = JsonConvert.SerializeObject(paginatedResult1, Formatting.Indented);
            return jsonResult1;
        }
        // ca nam
        public async Task<string> GradePointByClassAllYear(int idClass, int? semester = null)
        {
            var query = _appContext.GradePoints.Where(x => x.ClassId == idClass);

            if (semester.HasValue)
            {
                query = query.Where(x => x.Semester == (Semester)Enum.ToObject(typeof(Semester), semester));
            }

            var classGradePoints = query.ToList();
            var groupData = new List<StudentData>();
            var totalGroupData = groupData.Count();

            foreach (var gradePoint in classGradePoints)
            {
                var user = _appContext.Users.FirstOrDefault(u => u.Id == gradePoint.UserId);
                if (user == null)
                    continue;

                var existingUserData = groupData.FirstOrDefault(d => d.UserId == gradePoint.UserId);
                if (existingUserData != null)
                {
                    // Kiểm tra xem môn học đã tồn tại trong danh sách chưa
                    var existingSubject = existingUserData.SubjectClasses.FirstOrDefault(s => s.SubjectId == gradePoint.SubjectId);

                    if (existingSubject != null)
                    {

                        // Môn học đã tồn tại, cập nhật trung bình
                        existingSubject.Avegare = (existingSubject.Avegare + gradePoint.Average * 2) / 3;
                        // existingSubject.Rank = GetRank(gradePoint.Average);
                        existingUserData.TotalPoint += existingSubject.Avegare;

                    }
                    else
                    {
                        // Môn học chưa tồn tại, thêm mới vào danh sách
                        existingUserData.SubjectClasses.Add(new SubjectByClass
                        {
                            SubjectId = gradePoint.SubjectId,
                            SubjectName = _appContext.Subjects.FirstOrDefault(s => s.Id == gradePoint.SubjectId)?.Name,
                            Avegare = gradePoint.Average,
                            //Rank = GetRank(gradePoint.Average),


                        });
                        // existingUserData.TotalPoint += gradePoint.Average;
                    }

                }
                else
                {
                    // Người dùng chưa tồn tại trong groupData, tạo một mục mới
                    var userData = new StudentData
                    {
                        ClassId = gradePoint.ClassId,
                        ClassName = _appContext.Classes.FirstOrDefault(c => c.Id == gradePoint.ClassId)?.Name,
                        UserName = user.Name,
                        UserId = gradePoint.UserId,
                        SubjectClasses = new List<SubjectByClass>
                        {
                            new SubjectByClass
                            {
                                Semester = Convert.ToInt32(gradePoint.Semester),
                                SubjectId = gradePoint.SubjectId,
                                SubjectName = _appContext.Subjects.FirstOrDefault(s => s.Id == gradePoint.SubjectId)?.Name,
                                Avegare = gradePoint.Average
                            }
                        },


                    };
                    userData.TotalPoint = 0;

                    groupData.Add(userData);
                }

            }
            // float abc = 0;
            //  int count = 0;
            foreach (var userData in groupData)
            {
                //  abc +=  userData.TotalPoint ;
                // count++;
                userData.TotalPoint /= userData.SubjectClasses.Count();
                userData.Rank = GetRankCN(userData.TotalPoint, userData.SubjectClasses);
            };


            var paginatedResult = new Pagination<StudentData>
            {
                // Có thể cần cập nhật draw tại đây
                draw = totalGroupData,
                recordsFiltered = totalGroupData,
                recordsTotal = totalGroupData,
                data = groupData
            };

            var jsonResult = JsonConvert.SerializeObject(paginatedResult, Formatting.Indented);
            return jsonResult;
        }
        public async Task<List<StudentData>> GetRank(int idClass, int? semester = null)
        {
            var query = _appContext.GradePoints.Where(x => x.ClassId == idClass);

            if (semester.HasValue)
            {
                query = query.Where(x => x.Semester == (Semester)Enum.ToObject(typeof(Semester), semester));
            }

            var classGradePoints = query.ToList();
            var groupData = new List<StudentData>();
            var totalGroupData = groupData.Count();

            foreach (var gradePoint in classGradePoints)
            {
                var user = _appContext.Users.FirstOrDefault(u => u.Id == gradePoint.UserId);
                if (user == null)
                    continue;

                var existingUserData = groupData.FirstOrDefault(d => d.UserId == gradePoint.UserId);
                if (existingUserData != null)
                {
                    // Kiểm tra xem môn học đã tồn tại trong danh sách chưa
                    var existingSubject = existingUserData.SubjectClasses.FirstOrDefault(s => s.SubjectId == gradePoint.SubjectId);

                    if (existingSubject != null)
                    {

                        // Môn học đã tồn tại, cập nhật trung bình
                        existingSubject.Avegare = (existingSubject.Avegare + gradePoint.Average * 2) / 3;
                        // existingSubject.Rank = GetRank(gradePoint.Average);
                        existingUserData.TotalPoint += existingSubject.Avegare;

                    }
                    else
                    {
                        // Môn học chưa tồn tại, thêm mới vào danh sách
                        existingUserData.SubjectClasses.Add(new SubjectByClass
                        {
                            SubjectId = gradePoint.SubjectId,
                            SubjectName = _appContext.Subjects.FirstOrDefault(s => s.Id == gradePoint.SubjectId)?.Name,
                            Avegare = gradePoint.Average,
                            //Rank = GetRank(gradePoint.Average),


                        });
                        // existingUserData.TotalPoint += gradePoint.Average;
                    }

                }
                else
                {
                    // Người dùng chưa tồn tại trong groupData, tạo một mục mới
                    var userData = new StudentData
                    {
                        ClassId = gradePoint.ClassId,
                        ClassName = _appContext.Classes.FirstOrDefault(c => c.Id == gradePoint.ClassId)?.Name,
                        UserName = user.Name,
                        UserId = gradePoint.UserId,
                        SubjectClasses = new List<SubjectByClass>
                        {
                            new SubjectByClass
                            {
                                Semester = Convert.ToInt32(gradePoint.Semester),
                                SubjectId = gradePoint.SubjectId,
                                SubjectName = _appContext.Subjects.FirstOrDefault(s => s.Id == gradePoint.SubjectId)?.Name,
                                Avegare = gradePoint.Average
                            }
                        },


                    };
                    userData.TotalPoint = 0;

                    groupData.Add(userData);
                }

            }
            // float abc = 0;
            //  int count = 0;
            foreach (var userData in groupData)
            {
                //  abc +=  userData.TotalPoint ;
                // count++;
                userData.TotalPoint /= userData.SubjectClasses.Count();
                userData.Rank = GetRankCN(userData.TotalPoint, userData.SubjectClasses);
            };

            return groupData;
        }

        public  async Task<List<vm_class>> Get_By_Id_User_vm_class(int user_id)
        {
            try
            {
                var user_classes = _appContext.Students_Classes.SingleOrDefault(x => x.UserId == user_id);
                if (user_classes == null) return null!;
                var classes = await _appContext.Classes.Where(t => t.Id == user_classes!.ClassId).ProjectTo<vm_class>(_mapper.ConfigurationProvider).ToListAsync();
                if (classes == null) return null!;
                return classes;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Get_By_Id: {ex.Message}");
                throw;
            }
        }
    }
}
