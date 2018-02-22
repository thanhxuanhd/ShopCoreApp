using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using ShopCoreApp.Service.Interfaces;
using ShopCoreApp.Utilities.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShopCoreApp.Authorization
{
    public class BaseResourceAuthorizationHander : AuthorizationHandler<OperationAuthorizationRequirement, string>
    {

        #region Valiables
        private readonly IRoleService _roleSevice;
        #endregion
        #region Ctor
        public BaseResourceAuthorizationHander(IRoleService roleSevice)
        {
            _roleSevice = roleSevice;
        }
        #endregion

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, string resource)
        {
            var roles = ((ClaimsIdentity)context.User.Identity).Claims.FirstOrDefault(x => x.Type == CommonConstants.UserClaims.Roles);
            if (roles != null)
            {
                var listRole = roles.Value.Split(';');

                var hasPermission = await _roleSevice.CheckPermission(resource, requirement.Name, listRole);

                if (hasPermission || listRole.Contains(CommonConstants.AppRole.AdminRole)) {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }
            }
            else
            {
                context.Fail();
            }
        }
    }
}
