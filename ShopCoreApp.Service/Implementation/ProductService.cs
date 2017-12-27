using AutoMapper.QueryableExtensions;
using ShopCoreApp.Data.IRepositories;
using ShopCoreApp.Infrastructure.Enums;
using ShopCoreApp.Service.Interfaces;
using ShopCoreApp.Service.ViewModels;
using ShopCoreApp.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

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

        #endregion Ctor

        public List<ProductViewModel> GetAll()
        {
            return _productRepository.FindAll(x => x.ProductCategory).ProjectTo<ProductViewModel>().ToList();
        }

        public PageResult<ProductViewModel> GetAllPaging(int? categoryId, string keyword, int page = 1, int pageSize = 20)
        {
            var query = _productRepository.FindAll(x => x.Status == Status.Active);

            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword));

            if (categoryId.HasValue)
                query = query.Where(x => x.CategoryId == categoryId);

            int totalRow = query.Count();

            query = query.OrderByDescending(x => x.DateCreated)
                .Skip((page - 1) * pageSize).Take(pageSize);

            var data = query.ProjectTo<ProductViewModel>().ToList();
            var paginationSet = new PageResult<ProductViewModel>()
            {
                Result = data,
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = totalRow
            };
            return paginationSet;
        }
    }
}