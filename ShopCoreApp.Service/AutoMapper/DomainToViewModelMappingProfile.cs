using AutoMapper;
using ShopCoreApp.Data.Entities;
using ShopCoreApp.Service.ViewModels.Function;
using ShopCoreApp.Service.ViewModels.ProductCategory;

namespace ShopCoreApp.Service.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<ProductCategory, ProductCategoryViewModel>();
            CreateMap<Function, FunctionViewModel>();
        }
    }
}