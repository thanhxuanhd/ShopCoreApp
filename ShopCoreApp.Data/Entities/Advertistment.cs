using ShopCoreApp.Infrastructure.Enums;
using ShopCoreApp.Infrastructure.Interfaces;
using ShopCoreApp.Infrastructure.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopCoreApp.Data.Entities
{
    [Table("Advertistment")]
    public class Advertistment : DomainEntity<int>, ISwitchable, ISortable, IDateTracking
    {
        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        [StringLength(250)]
        public string Url { get; set; }

        [StringLength(20)]
        public string PositionId { get; set; }

        [ForeignKey("PositionId")]
        public virtual AdvertistmentPosition AdvertistmentPosition { get; set; }

        public Status Status { get; set; }
        public int SortOrder { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}