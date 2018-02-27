using ShopCoreApp.Service.ViewModels;
using ShopCoreApp.Service.ViewModels.Product;
using ShopCoreApp.Utilities.Dtos;
using System;
using System.Collections.Generic;

namespace ShopCoreApp.Service.Interfaces
{
    public interface IProductService : IDisposable
    {
        List<ProductViewModel> GetAll();

        PageResult<ProductViewModel> GetAllPaging(int? categoryId, string keyword, int page = 1, int pageSize = 20);

        ProductViewModel GetById(int id);

        ProductViewModel Add(ProductViewModel product);

        void Update(ProductViewModel product);

        void Delete(int id);

        void Save();

        void ImportExcel(string filePath, int categoryId);

        List<ProductExportViewModel> ExportProduct(int? categoryId);

        void AddQuantity(int productId, List<ProductQuantityViewModel> quantities);

        List<ProductQuantityViewModel> GetQuantities(int productId);
    }
}