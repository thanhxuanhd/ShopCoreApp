using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopCoreApp.Data.Entities;
using ShopCoreApp.Models.Account;
using ShopCoreApp.Utilities.Dtos;
using System.Threading.Tasks;

namespace ShopCoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger _logger;

        #region Contructor

        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ILogger<LoginController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        #endregion Contructor

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    ModelState.Clear();
                    return Redirect("Admin/Home/Index");
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    ModelState.AddModelError("", "Tài khoản bị khóa");
                    return View(model);
                }
                else
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}