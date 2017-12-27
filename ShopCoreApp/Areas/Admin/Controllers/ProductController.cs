﻿using Microsoft.AspNetCore.Mvc;
using ShopCoreApp.Service.Interfaces;

namespace ShopCoreApp.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        #region Variables
        private readonly IProductService _productService;
        #endregion
        #region Ctor
        public ProductController(IProductService productService)
        {
            _productService = productService;
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
        public IActionResult GetAllPaging(int? categoryId, string keyword, int page = 1, int pageSize = 20)
        {
            var model = _productService.GetAllPaging(categoryId, keyword, page, pageSize);
            return new ObjectResult(model);
        }
        #endregion
    }
}