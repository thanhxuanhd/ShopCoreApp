using ShopCoreApp.Infrastructure.SharedKernel;
using System.ComponentModel.DataAnnotations;

namespace ShopCoreApp.Data.Entities
{
    public class Color : DomainEntity<int>
    {
        [StringLength(250)]
        public string Name
        {
            get; set;
        }

        [StringLength(250)]
        public string Code { get; set; }
    }
}