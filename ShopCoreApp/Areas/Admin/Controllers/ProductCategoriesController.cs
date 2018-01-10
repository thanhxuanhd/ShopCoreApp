using Microsoft.AspNetCore.Mvc;
using ShopCoreApp.Service.Interfaces;
using System.Collections.Generic;

namespace ShopCoreApp.Areas.Admin.Controllers
{
    public class ProductCategoriesController : BaseController
    {
        #region Variables

        private readonly IProductCategoryService _productCategoryService;

        #endregion Variables

        #region Ctor

        public ProductCategoriesController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        #endregion Ctor

        #region View

        public IActionResult Index()
        {
            return View();
        }

        #endregion View

        #region AJAX Action

        public IActionResult GetAll()
        {
            var productCategories = _productCategoryService.GetAll();
            return new OkObjectResult(productCategories);
        }

        [HttpPost]
        public IActionResult ReOrder(int sourceId, int targetId)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }

            if (sourceId == targetId)
            {
                return new BadRequestResult();
            }

            _productCategoryService.ReOrder(sourceId, targetId);
            _productCategoryService.Save();

            return new OkResult();
        }

        [HttpPost]
        public IActionResult UpdateParentId(int sourceId, int targetId, Dictionary<int, int> items)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }

            if (sourceId == targetId)
            {
                return new BadRequestResult();
            }

            _productCategoryService.UpdateParentId(sourceId, targetId, items);
            _productCategoryService.Save();

            return new OkResult();
        }

        #endregion AJAX Action
    }
}