namespace ShopCoreApp.Infrastructure.Interfaces
{
    public interface IHasOwner<T>
    {
        T OwnerId { get; set; }
    }
}