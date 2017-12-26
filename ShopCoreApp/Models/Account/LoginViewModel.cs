using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopCoreApp.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Tên đăng nhập bắt buộc")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Mật khẩu bắt buộc")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
