using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopCoreApp.Data.Entities;
using ShopCoreApp.Data.MDEF.Extensions;

namespace ShopCoreApp.Data.MDEF.Configurations
{
    public class AdvertistmentPositionConfiguration : DbEntityConfiguration<AdvertistmentPosition>
    {
        public override void Configure(EntityTypeBuilder<AdvertistmentPosition> entity)
        {
            entity.Property(c => c.Id).HasMaxLength(50).IsRequired();
        }
    }
}