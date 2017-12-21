using ShopCoreApp.Infrastructure.SharedKernel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopCoreApp.Data.Entities
{
    public class ProductTag : DomainEntity<int>
    {
        public int ProductIds { get; set; }
        public string TagId { get; set; }

        [ForeignKey("ProductIds")]
        public virtual Product Product { get; set; }

        [ForeignKey("TagId")]
        [Column(TypeName = "varchar(255)")]
        public virtual Tag Tag { get; set; }
    }
}