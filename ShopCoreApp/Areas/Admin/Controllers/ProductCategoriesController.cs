using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopCoreApp.Service.Interfaces;

namespace ShopCoreApp.Areas.Admin.Controllers
{
    public class ProductCategoriesController : BaseController
    {
        #region Variables
        private readonly IProductCategoryService _productCategoryService;
        #endregion

        #region Ctor
        public ProductCategoriesController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }
        #endregion

        #region View
        public IActionResult Index()
        {
            return View();
        }
        #endregion
        #region AJAX Action
        public IActionResult GetAll() {

            var productCategories = _productCategoryService.GetAll();
            return new OkObjectResult(productCategories);
        }

        #endregion
    }
}