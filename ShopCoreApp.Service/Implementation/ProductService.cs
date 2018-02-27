using AutoMapper;
using AutoMapper.QueryableExtensions;
using OfficeOpenXml;
using ShopCoreApp.Data.Entities;
using ShopCoreApp.Data.IRepositories;
using ShopCoreApp.Infrastructure.Enums;
using ShopCoreApp.Infrastructure.Interfaces;
using ShopCoreApp.Service.Interfaces;
using ShopCoreApp.Service.ViewModels;
using ShopCoreApp.Service.ViewModels.Product;
using ShopCoreApp.Utilities.Constants;
using ShopCoreApp.Utilities.Dtos;
using ShopCoreApp.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ShopCoreApp.Service.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IProductTagRepository _productTagRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductQuantityRepository _productQuantityRepository;

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #region Ctor

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, 
            IProductTagRepository productTagRepository, ITagRepository tagRepository,
            IProductQuantityRepository productQuantityRepository)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _tagRepository = tagRepository;
            _productTagRepository = productTagRepository;
            _productQuantityRepository = productQuantityRepository;
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

        public ProductViewModel GetById(int id)
        {
            return Mapper.Map<Product, ProductViewModel>(_productRepository.FindById(id));
        }

        public ProductViewModel Add(ProductViewModel productVm)
        {
            var productTags = new List<ProductTag>();
            if (!string.IsNullOrEmpty(productVm.Tags))
            {
                string[] tags = productVm.Tags.Split(',');
                foreach (string tag in tags)
                {
                    var tagId = TextHelper.ToUnsignString(tag);
                    if (!_tagRepository.FindAll(x => x.Id == tagId).Any())
                    {
                        Tag tagEntity = new Tag()
                        {
                            Id = tagId,
                            Name = tag,
                            Type = CommonConstants.ProductTag
                        };
                        _tagRepository.Add(tagEntity);
                    }

                    ProductTag productTag = new ProductTag()
                    {
                        TagId = tagId
                    };
                    productTags.Add(productTag);
                }
                var product = Mapper.Map<ProductViewModel, Product>(productVm);
                foreach (var productTag in productTags)
                {
                    product.ProductTags.Add(productTag);
                }
                _productRepository.Add(product);
            }
            return productVm;
        }

        public void Update(ProductViewModel productVm)
        {
            var productTags = new List<ProductTag>();
            if (!string.IsNullOrEmpty(productVm.Tags))
            {
                string[] tags = productVm.Tags.Split(',');
                foreach (string tag in tags)
                {
                    var tagId = TextHelper.ToUnsignString(tag);
                    if (!_tagRepository.FindAll(x => x.Id == tagId).Any())
                    {
                        Tag tagEntity = new Tag()
                        {
                            Id = tagId,
                            Name = tag,
                            Type = CommonConstants.ProductTag
                        };
                        _tagRepository.Add(tagEntity);
                    }

                    ProductTag productTag = new ProductTag()
                    {
                        TagId = tagId
                    };
                    productTags.Add(productTag);
                }
                var product = Mapper.Map<ProductViewModel, Product>(productVm);
                foreach (var productTag in productTags)
                {
                    product.ProductTags.Add(productTag);
                }
                product.DateCreated = DateTime.UtcNow;
                _productRepository.Update(product);
            }
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _productRepository.Remove(id);
        }

        public void ImportExcel(string filePath, int categoryId)
        {
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                Product product;

                for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                {
                    product = new Product();
                    product.CategoryId = categoryId;

                    product.Name = workSheet.Cells[i, 1].Value.ToString();

                    product.Description = workSheet.Cells[i, 2].Value.ToString();

                    decimal.TryParse(workSheet.Cells[i, 3].Value.ToString(), out var originalPrice);
                    product.OriginalPrice = originalPrice;

                    decimal.TryParse(workSheet.Cells[i, 4].Value.ToString(), out var price);
                    product.Price = price;
                    decimal.TryParse(workSheet.Cells[i, 5].Value.ToString(), out var promotionPrice);

                    product.PromotionPrice = promotionPrice;
                    product.Content = workSheet.Cells[i, 6].Value.ToString();
                    product.SeoKeywords = workSheet.Cells[i, 7].Value.ToString();

                    product.SeoDescription = workSheet.Cells[i, 8].Value.ToString();
                    bool.TryParse(workSheet.Cells[i, 9].Value.ToString(), out var hotFlag);

                    product.HotFlag = hotFlag;
                    bool.TryParse(workSheet.Cells[i, 10].Value.ToString(), out var homeFlag);
                    product.HomeFlag = homeFlag;

                    product.Status = Status.Active;

                    _productRepository.Add(product);
                }
            }
        }

        public List<ProductExportViewModel> ExportProduct(int? categoryId)
        {
            var query = _productRepository.FindAll(x => x.ProductCategory);

            if (categoryId != null)
            {
                query = query.Where(x => x.CategoryId == categoryId);
            }

            return query.ProjectTo<ProductExportViewModel>().ToList();
        }

        public void AddQuantity(int productId, List<ProductQuantityViewModel> quantities)
        {
            _productQuantityRepository.RemoveMultiple(_productQuantityRepository.FindAll(x => x.ProductId == productId).ToList());
            foreach (var quantity in quantities)
            {
                _productQuantityRepository.Add(new ProductQuantity()
                {
                    ProductId = productId,
                    ColorId = quantity.ColorId,
                    SizeId = quantity.SizeId,
                    Quantity = quantity.Quantity
                });
            }
        }

        public List<ProductQuantityViewModel> GetQuantities(int productId)
        {
            return _productQuantityRepository.FindAll(x => x.ProductId == productId).ProjectTo<ProductQuantityViewModel>().ToList();
        }
    }
}