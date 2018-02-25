using ShopCoreApp.Data.Entities;
using ShopCoreApp.Data.IRepositories;

namespace ShopCoreApp.Data.EF.Repositories
{
    public class BillDetailRepository : EFRepository<BillDetail, int>, IBillDetailRepository
    {
        public BillDetailRepository(AppDbContext context) : base(context)
        {
        }
    }
}