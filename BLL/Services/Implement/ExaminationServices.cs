using AutoMapper;
using AutoMapper.QueryableExtensions;
using BLL.Services.Interface;
using BLL.ViewModels;
using BLL.ViewModels.Examination;
using Manager_Point.ApplicationDbContext;
using Manager_Point.Models;
using Newtonsoft.Json;
using System.Data.Entity;

namespace BLL.Services.Implement
{

    public class ExaminationServices : IExaminationServices
    {
        private readonly AppDbContext _appContext;
        private readonly IMapper _mapper;
        public ExaminationServices(IMapper mapper)
        {
            _appContext = new AppDbContext();
            _mapper = mapper;
        }
        public async Task<List<int>> Batch_Create_Item(List<vm_create_examination> requests)
        {
            try
            {
                var obj = _mapper.Map<List<Examination>>(requests);
                _appContext.Examinations.AddRange(obj);
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
                var examinationToDelete = await _appContext.Examinations.Where(t => ids.Contains(t.Id)).ToListAsync();

                if (examinationToDelete.Any())
                {
                    _appContext.Examinations.RemoveRange(examinationToDelete);
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

        public async Task<int> Create_Item(vm_create_examination request)
        {
            try
            {
                var obj = _mapper.Map<Examination>(request);
                _appContext.Examinations.AddRange(obj);
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
                var query = _appContext.Examinations
                    .Where(t => string.IsNullOrEmpty(search))
                    .Skip(skip)
                    .Take(page_size);
                var subjects = query.ToList();

                int totalCount = _appContext.Examinations
                    .Where(s => string.IsNullOrEmpty(search))
                    .Count();

                var vm_examination = _mapper.Map<List<vm_examination>>(_mapper.ConfigurationProvider).ToList();
                var paginatedResult = new PaginatedResult<vm_examination>
                {
                    TotalCount = totalCount,
                    PageNumber = page_number,
                    PageSize = page_size,
                    Data = vm_examination
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

        public async Task<vm_examination> Get_By_Id(int id)
        {

            try
            {
                var examination = await _appContext.GradePoints.ProjectTo<vm_examination>(_mapper.ConfigurationProvider).SingleOrDefaultAsync(x => x.Id == id);
                if (examination == null) return null!;
                return examination;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Get_By_Id: {ex.Message}");
                throw;
            }
        }

        public async Task<int> Modified_Item(int id, vm_update_examination request)
        {
            try
            {
                var objForUpdate = await _appContext.Examinations.FindAsync(id);
                if (objForUpdate == null) return -1;
                objForUpdate.GradePointId = request.GradePointId;
                objForUpdate.Point = request.Point;
                objForUpdate.TypePoint = request.TypePoint;
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
                var objToRemove = await _appContext.Examinations.FindAsync(id);
                // Xử lý trường hợp không tìm thấy đối tượng
                if (objToRemove == null) return false;

                // Kiểm tra xem đối tượng đã được theo dõi trong DbContext hay không
                var local = _appContext.Examinations.Local.FirstOrDefault(x => x.Id == id);

                // Sử dụng toán tử ba ngôi để xác định đối tượng cần xóa
                _appContext.Examinations.Remove(local != null ? local : objToRemove);

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
