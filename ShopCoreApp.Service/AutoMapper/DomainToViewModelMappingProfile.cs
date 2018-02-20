using AutoMapper;
using ShopCoreApp.Data.Entities;
using ShopCoreApp.Service.ViewModels;
using ShopCoreApp.Service.ViewModels.Function;
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
        }
    }
}