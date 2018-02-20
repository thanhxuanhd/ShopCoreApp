using ShopCoreApp.Data.Entities;
using ShopCoreApp.Data.IRepositories;

namespace ShopCoreApp.Data.EF.Repositories
{
    public class ProductTagRepository : EFRepository<ProductTag, int>, IProductTagRepository
    {
        public ProductTagRepository(AppDbContext context) : base(context)
        {
        }
    }
}