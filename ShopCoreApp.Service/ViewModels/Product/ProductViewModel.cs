using ShopCoreApp.Infrastructure.Enums;
using ShopCoreApp.Service.ViewModels.ProductCategory;
using ShopCoreApp.Service.ViewModels.ProductTag;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShopCoreApp.Service.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [StringLength(255)]
        public string Image { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal Price { get; set; }

        public decimal? PromotionPrice { get; set; }

        [Required]
        public decimal OriginalPrice { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public string Content { get; set; }
        public bool? HomeFlag { get; set; }
        public bool HotFlag { get; set; }
        public int? ViewCount { get; set; }

        public ProductCategoryViewModel ProductCategory { get; set; }
        public ICollection<ProductTagViewModel> ProductTags { set; get; }

        [StringLength(255)]
        public string Tags { get; set; }

        public string Unit { get; set; }
        public Status Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        [StringLength(255)]
        public string SeoPageTitle { get; set; }

        [StringLength(255)]
        public string SeoAlias { get; set; }

        public string SeoKeywords { get; set; }

        [StringLength(255)]
        public string SeoDescription { get; set; }
    }
}