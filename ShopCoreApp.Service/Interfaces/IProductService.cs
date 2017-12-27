using ShopCoreApp.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopCoreApp.Service.Interfaces
{
    public interface IProductService: IDisposable
    {
        List<ProductViewModel> GetAll();
    }
}
