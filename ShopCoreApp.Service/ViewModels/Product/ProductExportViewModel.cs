using ShopCoreApp.Infrastructure.Enums;
using ShopCoreApp.Service.ViewModels.ProductTag;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopCoreApp.Service.ViewModels.Product
{
    public class ProductExportViewModel
    {
        public ProductExportViewModel()
        {
            ProductTags = new List<ProductTagViewModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public decimal? PromotionPrice { get; set; }
        public decimal OriginalPrice { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public bool? HomeFlag { get; set; }
        public bool? HotFlag { get; set; }
        public int? ViewCount { get; set; }
        public string ProductCategory { get; set; }
        public List<ProductTagViewModel> ProductTags { set; get; }
        public string Tags { get; set; }
        public string Unit { get; set; }
        public Status Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string SeoPageTitle { get; set; }
        public string SeoAlias { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }
    }
}
