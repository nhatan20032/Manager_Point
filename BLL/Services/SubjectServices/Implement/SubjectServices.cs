using BLL.Repository;
using BLL.Services.SubjectServices.Interface;
using Manager_Point.ApplicationDbContext;
using Manager_Point.Models;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services.SubjectServices.Implement
{
    public class SubjectServices : ISubjectServices
    {
        protected readonly IRepository<Subject> _repositories;
        protected readonly AppDbContext _dbcontext;
        public SubjectServices(AppDbContext appDbContext)
        {
            _repositories = new Repository<Subject>();
            _dbcontext = appDbContext;
        }
        public async Task<Subject> AddItem(Subject request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request), "Request object cannot be null");
            try
            {
                var obj = new Subject()
                {
                    Name = request.Name,
                    Description = request.Description,
                    Status = request.Status,
                };
                var result = await _repositories.CreateAsync(obj);
                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error occurred while adding Subject.", ex);
            }
        }

        public async Task<List<Subject>> CreateMultipleAsync(List<Subject> entities)
        {
            if (entities == null || !entities.Any()) throw new ArgumentNullException(nameof(entities), "Entity list cannot be null or empty");
            try
            {
                var addRoles = entities.Where(entity => entity != null)
                    .Select(entity => new Subject
                    {
                        Name = entity.Name,
                        Description = entity.Description,
                        Status = entity.Status
                    })
                    .ToList();

                // Gọi phương thức CreateMultipleAsync với await
                var result = await _repositories.CreateMultipleAsync(addRoles);
                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error occurred while adding Role.", ex);
            }
        }

        public async Task<string> GetAllAsync(int pageNumber = 1, int pageSize = 10, string search = "")
        {
            try
            {
                var query = _dbcontext.Roles.AsQueryable();

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(r => r.Name!.Contains(search));
                }

                var totalItems = await query.CountAsync();

                var roles = await query.Skip((pageNumber - 1) * pageSize)
                                       .Take(pageSize)
                                       .AsNoTracking()
                                       .ToListAsync();

                var result = new
                {
                    pageSize = pageSize,
                    pageNumber = pageNumber,
                    totalItems = totalItems,
                    data = roles
                };

                return Newtonsoft.Json.JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error occurred while getting Role.", ex);
            }
        }

        public async Task<Subject> GetByIdAsync(int id)
        {
            try
            {
                var result = await _repositories.GetByIdAsync(id);
                return result!;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error occurred while getting Role.", ex);
            }
        }

        public async Task<Subject> ModifiedItem(int id, Subject entity)
        {
            try
            {
                var result = await _repositories.UpdateAsync(id, entity);
                return result!;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error occurred while modifing Role.", ex);
            }
        }

        public async Task<Subject> RemoveItem(int id)
        {
            try
            {
                var result = await _repositories.RemoveAsync(id);
                return result!;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error occurred while removing Role.", ex);
            }
        }

        public async Task<List<Subject>> RemoveMultipleAsync(List<int> ids)
        {
            try
            {
                var result = await _repositories.RemoveMultipleAsync(ids);
                return result!;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error occurred while removing Role.", ex);
            }
        }
    }
}
