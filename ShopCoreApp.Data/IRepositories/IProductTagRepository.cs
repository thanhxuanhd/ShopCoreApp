using ShopCoreApp.Data.Entities;
using ShopCoreApp.Infrastructure.Interfaces;

namespace ShopCoreApp.Data.IRepositories
{
    public interface IProductTagRepository : IRepository<ProductTag, int>
    {
    }
}