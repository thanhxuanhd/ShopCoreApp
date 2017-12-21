using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ShopCoreApp.Data.Entities;
using System;
using System.Threading.Tasks;

namespace ShopCoreApp.Pages.Account.Manage
{
    public class ResetAuthenticatorModel : PageModel
    {
        private UserManager<AppUser> _userManager;
        private ILogger<ResetAuthenticatorModel> _logger;

        public ResetAuthenticatorModel(
            UserManager<AppUser> userManager,
            ILogger<ResetAuthenticatorModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await _userManager.SetTwoFactorEnabledAsync(user, false);
            await _userManager.ResetAuthenticatorKeyAsync(user);
            _logger.LogInformation("User with ID '{UserId}' has reset their authentication app key.", user.Id);

            return RedirectToPage("./EnableAuthenticator");
        }
    }
}