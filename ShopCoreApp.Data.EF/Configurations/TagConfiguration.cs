using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopCoreApp.Data.EF.Extensions;
using ShopCoreApp.Data.Entities;

namespace ShopCoreApp.Data.EF.Configurations
{
    public class TagConfiguration : DbEntityConfiguration<Tag>
    {
        public override void Configure(EntityTypeBuilder<Tag> entity)
        {
            entity.Property(c => c.Id).HasMaxLength(50)
                .IsRequired().HasColumnType("varchar(50)");
        }
    }
}