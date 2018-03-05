﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using ShopCoreApp.Authorization;
using ShopCoreApp.Service.Interfaces;
using ShopCoreApp.Service.ViewModels;
using ShopCoreApp.Service.ViewModels.Product;
using ShopCoreApp.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ShopCoreApp.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        #region Variables

        private readonly IProductService _productService;
        private readonly IProductCategoryService _productCategoryService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IAuthorizationService _authorizationService;
        #endregion Variables

        #region Ctor

        public ProductController(IProductService productService,
            IProductCategoryService productCategoryService,
            IHostingEnvironment hostingEnvironment,
            IAuthorizationService authorizationService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
            _hostingEnvironment = hostingEnvironment;
            _authorizationService = authorizationService;
        }

        #endregion Ctor

        #region Action

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Product";
            var result = await _authorizationService.AuthorizeAsync(User, "PRODUCT", Operations.Read);
            if (result.Succeeded == false)
            {
                return new RedirectResult("/Admin/Login/Index");
            }
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

        [HttpPost]
        public IActionResult ImportExcel(IList<IFormFile> files, int categoryId)
        {
            if (files != null && files.Count > 0)
            {
                var file = files[0];
                var fileName = ContentDispositionHeaderValue
                                .Parse(file.ContentDisposition)
                                .FileName
                                .Trim('"');
                var folder = _hostingEnvironment.WebRootPath + $@"\uploaded\excels";

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string filePath = Path.Combine(folder, fileName);

                using (FileStream fs = System.IO.File.Create(filePath))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }

                _productService.ImportExcel(filePath, categoryId);
                _productService.Save();

                return new OkObjectResult(filePath);
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult ExportExcel(int? categoryId)
        {
            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string directory = Path.Combine(sWebRootFolder, "export-files");
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            string sFileName = $"Product_{DateTime.Now:yyyyMMddhhmmss}.xlsx";
            string fileUrl = $"{Request.Scheme}://{Request.Host}/export-files/{sFileName}";
            FileInfo file = new FileInfo(Path.Combine(directory, sFileName));
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            }
            var products = _productService.ExportProduct(categoryId);
            using (ExcelPackage package = new ExcelPackage(file))
            {
                // add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Products");
                worksheet.Cells["A1"].LoadFromCollection(products, true, TableStyles.Light1);
                worksheet.Cells.AutoFitColumns();
                package.Save(); //Save the workbook.
            }
            return new OkObjectResult(fileUrl);
        }

        [HttpPost]
        public IActionResult SaveQuantities(int productId, List<ProductQuantityViewModel> quantities)
        {
            _productService.AddQuantity(productId, quantities);
            _productService.Save();
            return new OkObjectResult(quantities);
        }

        [HttpGet]
        public IActionResult GetQuantities(int productId)
        {
            var quantities = _productService.GetQuantities(productId);
            return new OkObjectResult(quantities);
        }

        [HttpPost]
        public IActionResult SaveImages(int productId, string[] images)
        {
            _productService.AddImages(productId, images);
            _productService.Save();
            return new OkObjectResult(images);
        }

        [HttpGet]
        public IActionResult GetImages(int productId)
        {
            var images = _productService.GetImages(productId);
            return new OkObjectResult(images);
        }

        [HttpPost]
        public IActionResult SaveWholePrice(int productId, List<WholePriceViewModel> wholePrices)
        {
            _productService.AddWholePrice(productId, wholePrices);
            _productService.Save();
            return new OkObjectResult(wholePrices);
        }

        [HttpGet]
        public IActionResult GetWholePrices(int productId)
        {
            var wholePrices = _productService.GetWholePrices(productId);
            return new OkObjectResult(wholePrices);
        }

        #endregion AJAX API
    }
}