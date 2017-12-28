using ShopCoreApp.Data.Entities;
using ShopCoreApp.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopCoreApp.Data.MDEF.Repositories
{
    public class ProductMDRepository : MDEFRepository<Product, int>, IProductRepository
    {
        public ProductMDRepository(AppMDDbContext context) : base(context)
        {
        }
    }

}
