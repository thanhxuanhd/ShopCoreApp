using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopCoreApp.Data.EF.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void AddConfigruation<TEntity>(this ModelBuilder modelBuilder,
            DbEnityConfiguration<TEntity> enityConfiguration) where TEntity : class
        {
            modelBuilder.Entity<TEntity>(enityConfiguration.Configure);
        }

        public abstract class DbEnityConfiguration<TEntity> where TEntity : class
        {
            public abstract void Configure(EntityTypeBuilder<TEntity> entityTypeBuilder);
        }
    }
}
