using ShopCoreApp.Infrastructure.Enums;
using ShopCoreApp.Infrastructure.Interfaces;
using ShopCoreApp.Infrastructure.SharedKernel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopCoreApp.Data.Entities
{
    [Table("ProductCategory")]
    public class ProductCategory : DomainEntity<int>, IHasSeoMetaData, ISwitchable, ISortable
    {
        public ProductCategory()
        {
            Products = new List<Product>();
        }

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

        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string SeoAlias { set; get; }

        public string SeoKeywords { set; get; }
        public string SeoDescription { set; get; }
        public virtual ICollection<Product> Products { get; set; }
    }
}