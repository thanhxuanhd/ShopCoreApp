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
using Microsoft.AspNetCore.Authorization;
using ShopCoreApp.Authorization;

namespace ShopCoreApp.Areas.Admin.Components
{
    public class SideBarViewComponent : ViewComponent
    {
        #region Variables
        private readonly IFunctionService _functionService;
        private readonly IAuthorizationService _authorizationService;
        #endregion

        #region Ctor
        public SideBarViewComponent(IFunctionService functionService, IAuthorizationService authorizationService)
        {
            _functionService = functionService;
            _authorizationService = authorizationService;
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
                var functionsTemp = await _functionService.GetAll(string.Empty);

                if (functionsTemp.Any())
                {
                    functionsTemp.ForEach(async x =>
                    {
                        var havePermission = await _authorizationService.AuthorizeAsync((ClaimsPrincipal)User, x.Id, Operations.Read);
                        if (havePermission.Succeeded)
                        {
                            functions.Add(x);
                        }
                    });
                }
            }
            return View(functions);
        }
        #endregion
    }
}
