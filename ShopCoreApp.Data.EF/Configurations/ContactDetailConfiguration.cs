using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopCoreApp.Data.Entities;
using ShopCoreApp.Data.EF.Extensions;

namespace ShopCoreApp.Data.EF.Configurations
{
    public class ContactDetailConfiguration : DbEntityConfiguration<Contact>
    {
        public override void Configure(EntityTypeBuilder<Contact> entity)
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).HasMaxLength(255).IsRequired();
            // etc.
        }
    }
}