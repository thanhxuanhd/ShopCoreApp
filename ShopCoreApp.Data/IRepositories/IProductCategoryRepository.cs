using ShopCoreApp.Data.Entities;
using ShopCoreApp.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopCoreApp.Data.EF.Repositories
{
    public interface IProductCategoryRepository: IRepository<ProductCategory, int>
    {
        List<ProductCategory> GetByAlias(string alias);
    }
}
