using ShopCoreApp.Data.Entities;
using ShopCoreApp.Data.IRepositories;

namespace ShopCoreApp.Data.EF.Repositories
{
    public class ColorRepository : EFRepository<Color, int>, IColorRepository
    {
        public ColorRepository(AppDbContext context) : base(context)
        {
        }
    }
}