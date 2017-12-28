using ShopCoreApp.Data.EF.Repositories;
using ShopCoreApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopCoreApp.Data.MDEF.Repositories
{
    public class ProductCategoryMDRepositoty : MDEFRepository<ProductCategory, int>, IProductCategoryRepository
    {
        AppMDDbContext _context;
        public ProductCategoryMDRepositoty(AppMDDbContext context) : base(context)
        {
            _context = context;
        }

        public List<ProductCategory> GetByAlias(string alias)
        {
            return _context.ProductCategories.Where(x => x.SeoAlias == alias).ToList();
        }
    }
}
