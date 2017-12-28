using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopCoreApp.Data.Entities;
using ShopCoreApp.Data.MDEF.Extensions;

namespace ShopCoreApp.Data.MDEF.Configurations
{
    public class SystemConfigConfiguration : DbEntityConfiguration<SystemConfig>
    {
        public override void Configure(EntityTypeBuilder<SystemConfig> entity)
        {
            entity.Property(c => c.Id).HasMaxLength(255).IsRequired();
            // etc.
        }
    }
}