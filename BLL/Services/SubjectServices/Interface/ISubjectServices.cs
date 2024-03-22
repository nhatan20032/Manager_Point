using Manager_Point.Models;

namespace BLL.Services.SubjectServices.Interface
{
    public interface ISubjectServices
    {
        public Task<Subject> AddItem(Subject request);
        public Task<Subject> ModifiedItem(int id, Subject entity);
        public Task<Subject> RemoveItem(int id);
        public Task<List<Subject>> CreateMultipleAsync(List<Subject> entities);
        public Task<List<Subject>> RemoveMultipleAsync(List<int> ids);
        public Task<string> GetAllAsync(int pageNumber, int pageSize, string search);
        public Task<Subject> GetByIdAsync(int id);
    }
}
