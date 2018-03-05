using AutoMapper;
using ShopCoreApp.Data.Entities;
using ShopCoreApp.Service.ViewModels;
using ShopCoreApp.Service.ViewModels.Bill;
using ShopCoreApp.Service.ViewModels.Function;
using ShopCoreApp.Service.ViewModels.Product;
using ShopCoreApp.Service.ViewModels.ProductCategory;
using ShopCoreApp.Service.ViewModels.System;

namespace ShopCoreApp.Service.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<ProductCategory, ProductCategoryViewModel>();
            CreateMap<Function, FunctionViewModel>();
            CreateMap<Product, ProductViewModel>();
            CreateMap<AppUser, AppUserViewModel>();
            CreateMap<AppRole, AppRoleViewModel>();
            CreateMap<Permission, PermissionViewModel>();
            CreateMap<Product, ProductExportViewModel>()
                .ForMember(pd => pd.ProductCategory, opts => opts.MapFrom(o => o.ProductCategory.Name));
            CreateMap<Bill, BillViewModel>();
            CreateMap<BillDetail, BillDetailViewModel>();
            CreateMap<Color, ColorViewModel>();
            CreateMap<Size, SizeViewModel>();
            CreateMap<ProductQuantity, ProductQuantityViewModel>();
            CreateMap<ProductImage, ProductImageViewModel>();
            CreateMap<WholePrice, WholePriceViewModel>();
        }
    }
}