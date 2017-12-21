using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopCoreApp.Data.EF.Extensions;
using ShopCoreApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopCoreApp.Data.EF.Configurations
{
    public class AnnouncementConfiguration : DbEntityConfiguration<Announcement>
    {
        public override void Configure(EntityTypeBuilder<Announcement> entity)
        {
            entity.Property(c => c.Id).HasMaxLength(128).IsRequired();
        }
    }
}
