using ShopCoreApp.Service.ViewModels.Tag;

namespace ShopCoreApp.Service.ViewModels.ProductTag
{
    public class ProductTagViewModel
    {
        public int ProductIds { get; set; }
        public string TagId { get; set; }

        public ProductViewModel Product { get; set; }

        public virtual TagViewModel Tag { get; set; }
    }
}