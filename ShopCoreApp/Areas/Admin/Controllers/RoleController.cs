using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopCoreApp.Service.Interfaces;

namespace ShopCoreApp.Areas.Admin.Controllers
{
    public class RoleController : BaseController
    {
        #region Valiables
        private readonly IRoleService _roleService;
        #endregion

        #region Ctor
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        #endregion

        #region View
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region AJAX Action
        public async Task<IActionResult> GetAll()
        {
            var roles = await _roleService.GetAllAsync();
            return new OkObjectResult(roles);
        }
        #endregion

    }
}