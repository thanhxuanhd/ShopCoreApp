using ShopCoreApp.Service.ViewModels.Function;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopCoreApp.Service.Interfaces
{
    public interface IFunctionService : IDisposable
    {
        Task<List<FunctionViewModel>> GetAll();

        List<FunctionViewModel> GetAllByPermission(Guid userId);
    }
}