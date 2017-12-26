using Microsoft.AspNetCore.Mvc;
using ShopCoreApp.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopCoreApp.Extensions;
using System.Security.Claims;
using ShopCoreApp.Service.ViewModels.Function;
using static ShopCoreApp.Utilities.Constants.CommonConstants;

namespace ShopCoreApp.Areas.Admin.Components
{
    public class SideBarViewComponent : ViewComponent
    {
        private readonly IFunctionService _functionService;
        #region Ctor
        public SideBarViewComponent(IFunctionService functionService)
        {
            _functionService = functionService;
        }
        #endregion

        #region Method
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var roles = ((ClaimsPrincipal)User).GetSpecificClaim("Roles");
            List<FunctionViewModel> functions = new List<FunctionViewModel>();
            if (roles.Split(";").Contains(AppRole.AdminRole))
            {
                functions = await _functionService.GetAll();
            }
            else
            {
                // TODO
                // _functionService.GetAllByPermission();

            }
            return View(functions);
        }
        #endregion
    }
}
