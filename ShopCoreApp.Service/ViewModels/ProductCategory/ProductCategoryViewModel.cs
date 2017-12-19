using ShopCoreApp.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopCoreApp.Service.ViewModels.ProductCategory
{
    public class ProductCategoryViewModel
    {

        public ProductCategoryViewModel()
        {
            Products = new List<ProductViewModel>();
        }

        public int Id { get; set; }

        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        public int? ParentId { get; set; }
        public int? HomeOrder { get; set; }
        public string Image { get; set; }
        public bool? HomeFlag { get; set; }
        public int SortOrder { get; set; }
        public Status Status { set; get; }
        public string SeoPageTitle { get; set; }

        [StringLength(255)]
        public string SeoAlias { set; get; }

        public string SeoKeywords { set; get; }
        public string SeoDescription { set; get; }
        public ICollection<ProductViewModel> Products { get; set; }
    }
}
