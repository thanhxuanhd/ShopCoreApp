using ShopCoreApp.Service.ViewModels.System;
using ShopCoreApp.Utilities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopCoreApp.Service.Interfaces
{
    public interface IUserService
    {
        Task<bool> AddAsync(AppUserViewModel userVm);

        Task DeleteAsync(string id);

        Task<List<AppUserViewModel>> GetAllAsync();

        PageResult<AppUserViewModel> GetAllPagingAsync(string keyword, int page = 0, int pageSize = 15);

        Task<AppUserViewModel> GetByIdAsync(string id);

        Task UpdateAsync(AppUserViewModel userVm);
    }
}