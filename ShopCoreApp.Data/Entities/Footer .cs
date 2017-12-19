using ShopCoreApp.Infrastructure.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopCoreApp.Data.Entities
{
    [Table("Footers")]
    public class Footer : DomainEntity<string>
    {
        [Required]
        public string Content { set; get; }
    }
}