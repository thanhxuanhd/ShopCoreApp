using ShopCoreApp.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using static ShopCoreApp.Data.EF.Extensions.ModelBuilderExtensions;
using Microsoft.EntityFrameworkCore;

namespace ShopCoreApp.Data.EF.Configurations
{
    public class TagConfiguration : DbEnityConfiguration<Tag>
    {
        public override void Configure(EntityTypeBuilder<Tag> entityTypeBuilder)
        {
            entityTypeBuilder.Property(c => c.Id).HasMaxLength(50)
            .IsRequired().HasColumnType("varchar(50)");
        }
    }
}
