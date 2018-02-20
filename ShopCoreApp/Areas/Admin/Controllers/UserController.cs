﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ShopCoreApp.Service.Interfaces;
using ShopCoreApp.Service.ViewModels.System;

namespace ShopCoreApp.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        #region Valiables
        private readonly IUserService _userService;
        #endregion

        #region Ctor
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region View
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region AJAX Action
        public IActionResult GetAll()
        {
            var model = _userService.GetAllAsync();

            return new OkObjectResult(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            var model = await _userService.GetByIdAsync(id);

            return new OkObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetAllPaging(string keyword, int page, int pageSize)
        {
            var model = _userService.GetAllPagingAsync(keyword, page, pageSize);
            return new OkObjectResult(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveEntity(AppUserViewModel userVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            else
            {
                if (userVm.Id == null)
                {
                    await _userService.AddAsync(userVm);
                }
                else
                {
                    await _userService.UpdateAsync(userVm);
                }
                return new OkObjectResult(userVm);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                await _userService.DeleteAsync(id);

                return new OkObjectResult(id);
            }
        }
        #endregion

    }
}