using ShopCoreApp.Service.ViewModels;
using ShopCoreApp.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopCoreApp.Service.Interfaces
{
    public interface IProductService : IDisposable
    {
        List<ProductViewModel> GetAll();

        PageResult<ProductViewModel> GetAllPaging(int? categoryId, string keyword, int page = 1, int pageSize = 20);
    }
}
