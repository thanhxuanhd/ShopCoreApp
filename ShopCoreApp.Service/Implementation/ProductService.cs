using AutoMapper.QueryableExtensions;
using ShopCoreApp.Data.IRepositories;
using ShopCoreApp.Service.Interfaces;
using ShopCoreApp.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopCoreApp.Service.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #region Ctor
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        #endregion
        public List<ProductViewModel> GetAll()
        {
            return _productRepository.FindAll(x=>x.ProductCategory).ProjectTo<ProductViewModel>().ToList();
        }
    }
}
