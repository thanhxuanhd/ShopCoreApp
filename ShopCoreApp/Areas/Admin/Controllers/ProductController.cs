using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ShopCoreApp.Service.Interfaces;
using ShopCoreApp.Service.ViewModels;
using ShopCoreApp.Utilities.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace ShopCoreApp.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        #region Variables

        private readonly IProductService _productService;
        private readonly IProductCategoryService _productCategoryService;

        #endregion Variables

        #region Ctor

        public ProductController(IProductService productService, IProductCategoryService productCategoryService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
        }

        #endregion Ctor

        #region Action

        public IActionResult Index()
        {
            ViewData["Title"] = "Product";
            return View();
        }

        #endregion Action

        #region AJAX API

        [HttpGet]
        public IActionResult GetAll()
        {
            var model = _productService.GetAll();
            return new ObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetAllCategory()
        {
            var categories = _productCategoryService.GetAll();
            return new ObjectResult(categories);
        }

        [HttpGet]
        public IActionResult GetAllPaging(int? categoryId, string keyword, int page = 1, int pageSize = 20)
        {
            var model = _productService.GetAllPaging(categoryId, keyword, page, pageSize);
            return new ObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> errors = ModelState.Values.SelectMany(x => x.Errors);
                return new BadRequestObjectResult(errors);
            }

            var product = _productService.GetById(id);
            return new OkObjectResult(product);
        }

        [HttpPost]
        public IActionResult SaveEntity(ProductViewModel productVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> errors = ModelState.Values.SelectMany(x => x.Errors);
                return new BadRequestObjectResult(errors);
            }

            productVm.SeoAlias = TextHelper.ToUnsignString(productVm.Name);

            if (productVm.Id == 0)
            {
                _productService.Add(productVm);
            }
            else
            {
                _productService.Update(productVm);
            }

            _productService.Save();

            return new OkObjectResult(productVm);
        }

        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> errors = ModelState.Values.SelectMany(x => x.Errors);
                return new BadRequestObjectResult(errors);
            }

            _productService.Delete(id);
            _productService.Save();

            return new OkObjectResult(id);
        }

        #endregion AJAX API
    }
}