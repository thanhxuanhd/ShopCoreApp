using Microsoft.AspNetCore.Mvc;
using ShopCoreApp.Service.Interfaces;
using ShopCoreApp.Service.ViewModels.ProductCategory;
using ShopCoreApp.Utilities.Helpers;
using System.Collections.Generic;
using System.Linq;

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

        [HttpGet]
        public IActionResult GetAll()
        {
            var productCategories = _productCategoryService.GetAll();
            return new OkObjectResult(productCategories);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var productCategory = _productCategoryService.GetById(id);

            return new OkObjectResult(productCategory);
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

        [HttpPost]
        public IActionResult SaveProductCategory(ProductCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                                       .SelectMany(x => x.Errors)
                                       .Select(x => x.ErrorMessage);
                return BadRequest(errors);
            }
            if (!string.IsNullOrEmpty(model.SeoAlias))
            {
                model.SeoAlias = TextHelper.ToUnsignString(model.SeoAlias);
            }

            if (model.Id == 0)
            {
                var productCategory = _productCategoryService.Add(model);
            }
            else
            {
                _productCategoryService.Update(model);
            }

            _productCategoryService.Save();
            return new OkObjectResult(model);
        }

        [HttpDelete]
        public IActionResult DeleteProductCategory(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            _productCategoryService.Delete(id);
            _productCategoryService.Save();
            return new OkObjectResult(id);
        }

        #endregion AJAX Action
    }
}