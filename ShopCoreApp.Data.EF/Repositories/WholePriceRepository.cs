using ShopCoreApp.Data.Entities;
using ShopCoreApp.Data.IRepositories;

namespace ShopCoreApp.Data.EF.Repositories
{
    public class WholePriceRepository : EFRepository<WholePrice, int>, IWholePriceRepository
    {
        public WholePriceRepository(AppDbContext context) : base(context)
        {
        }
    }
}