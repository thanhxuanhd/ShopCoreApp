﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopCoreApp.Data.EF.Extensions;
using ShopCoreApp.Data.Entities;

namespace ShopCoreApp.Data.EF.Configurations
{
    public class PageConfiguration : DbEntityConfiguration<Page>
    {
        public override void Configure(EntityTypeBuilder<Page> entity)
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).HasMaxLength(255).IsRequired();
            // etc.
        }
    }
}