﻿using AutoMapper;
using ShopCoreApp.Data.Entities;
using ShopCoreApp.Service.ViewModels.ProductCategory;

namespace ShopCoreApp.Service.AutoMapper
{
    public class ViewModelToDomainMappingProfile: Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ProductCategoryViewModel, ProductCategory>().ConstructUsing(c=>new ProductCategory(c.Name,c.Description,c.ParentId,c.HomeOrder,c.Image,c.HomeFlag,c.SortOrder,c.Status,c.SeoPageTitle,c.SeoAlias,c.SeoKeywords,c.SeoDescription));
        }
    }
}