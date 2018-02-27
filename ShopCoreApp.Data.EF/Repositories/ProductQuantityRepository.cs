using ShopCoreApp.Data.Entities;
using ShopCoreApp.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopCoreApp.Data.EF.Repositories
{
    public class ProductQuantityRepository : EFRepository<ProductQuantity, int>, IProductQuantityRepository
    {
        public ProductQuantityRepository(AppDbContext context) : base(context)
        {
        }
    }
}
