using Azure.Core;
using BLL.Services.RoleServices.Interface;
using Data.Repository;
using Manager_Point.ApplicationDbContext;
using Manager_Point.Models;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services.RoleServices.Impliment
{
    public class RoleServices : IRoleServices
    {
        protected readonly IRepository<Role> _repositories;
        protected readonly AppDbContext _dbcontext;
        public RoleServices(Repository<Role> repository, AppDbContext appDbContext)
        {
            _repositories = repository;
            _dbcontext = appDbContext;
        }

        public async Task<Role> AddItem(Role request)
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
                var result = await _repositories.CreateAsync(obj);
                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error occurred while adding Role.", ex);
            }
        }

        public Task<List<Role>> CreateMultipleAsync(List<Role> entities)
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
                var result = _repositories.CreateMultipleAsync(addRoles);
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


        public async Task<Role> GetByIdAsync(int id)
        {
            var result = await _repositories.GetByIdAsync(id);
            return result;
        }

        public async Task<Role> ModifiedItem(int id, Role entity)
        {
            var result = await _repositories.UpdateAsync(id, entity);
            return result;
        }

        public async Task<Role> RemoveItem(int id)
        {
            var result = await _repositories.RemoveAsync(id);
            return result;
        }

        public async Task<List<Role>> RemoveMultipleAsync(List<int> ids)
        {
            var result = await _repositories.RemoveMultipleAsync(ids);
            return result;
        }
    }
}
