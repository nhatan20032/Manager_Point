using BLL.Repository;
using BLL.Services.RoleServices.Interface;
using BLL.ViewModels.Role;
using Manager_Point.ApplicationDbContext;
using Manager_Point.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BLL.Services.RoleServices.Impliment
{
    public class RoleServices : IRoleServices
    {
        protected readonly IRepository<Role> _repositories;
        protected readonly AppDbContext _dbcontext;
        public RoleServices(AppDbContext appDbContext)
        {
            _repositories = new Repository<Role>();
            _dbcontext = appDbContext;
        }

        public async Task<string> AddItem(VM_Create_Roles request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request), "Request object cannot be null");
            try
            {
                var obj = new Role()
                {
                    Name = request.Name,
                    Description = request.Description,
                    Status = request.Status,
                };
                await _repositories.CreateAsync(obj);
                return "Success";
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error occurred while adding Role.", ex);
            }
        }

        public async Task<string> CreateMultipleAsync(List<Role> entities)
        {
            if (entities == null || !entities.Any()) throw new ArgumentNullException(nameof(entities), "Entity list cannot be null or empty");
            try
            {
                var addRoles = entities.Where(entity => entity != null)
                    .Select(entity => new Role
                    {
                        Name = entity.Name,
                        Description = entity.Description,
                        Status = entity.Status
                    })
                    .ToList();

                // Gọi phương thức CreateMultipleAsync với await
                await _repositories.CreateMultipleAsync(addRoles);
                return "Success";
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

                var roles = await query
                                .Select(r => new
                                {
                                    Id = r.Id,
                                    Name = r.Name,
                                    Description = r.Description,
                                    Status = r.Status
                                })
                                .Skip((pageNumber - 1) * pageSize)
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
                var json = JsonConvert.SerializeObject(result, Formatting.Indented);
                json = json.Replace("\\r\\n", Environment.NewLine);
                return json;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error occurred while getting Role.", ex);
            }
        }


        public async Task<Role> GetByIdAsync(int id)
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

        public async Task<string> ModifiedItem(int id, Role entity)
        {
            try
            {
                await _repositories.UpdateAsync(id, entity);
                return "Success";
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error occurred while modifing Role.", ex);
            }

        }

        public async Task<string> RemoveItem(int id)
        {
            try
            {
                await _repositories.RemoveAsync(id);
                return "Success";
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error occurred while removing Role.", ex);
            }
;
        }

        public async Task<string> RemoveMultipleAsync(List<int> ids)
        {
            try
            {
                await _repositories.RemoveMultipleAsync(ids);
                return "Success";
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error occurred while removing Role.", ex);
            }
        }
    }
}
