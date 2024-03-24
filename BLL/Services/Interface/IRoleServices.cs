﻿using BLL.ViewModels.Role;
using BLL.ViewModels.Subject;

namespace BLL.Services.Interface
{
    public interface IRoleServices
    {
        public Task<string> Get_All_Async(int page_number = 1, int page_size = 10, string search = "");
        public Task<vm_role> Get_By_Id(int id);
        public Task<int> Create_Item(vm_create_role request);
        public Task<List<int>> Batch_Create_Item(List<vm_create_role> requests);
        public Task<int> Modified_Item(int id, vm_update_role request);
        public Task<bool> Remove_Item(int id);
        public Task<bool> Batch_Remove_Item(List<int> ids);
    }
}
