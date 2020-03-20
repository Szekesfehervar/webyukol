using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Manage.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ukol_15_3_2018.Model;

namespace ukol_15_3_2018
{
    public class NewPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signinManager;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Heslo musí mít délku mezi 6 a 100 znaky.", MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Hesla se musí shodovat.")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Email { get; set; }

        public NewPasswordModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signinManager)
        {
            _userManager = userManager;
            _signinManager = signinManager;
        }
        [TempData]
        public string SuccessMessage { get; set; }
        [TempData]
        public string FailureMessage { get; set; }
        public string ReturnUrl { get; set; }
   
        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("./");
            if (ModelState.IsValid && Email != null)
            {
                var user = await _userManager.FindByEmailAsync(Email);
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                if (user == null)
                {
                    FailureMessage = "Unknown Email";
                    return Page();
                }
                var result = await _userManager.ConfirmEmailAsync(user, Code);
                if (!result.Succeeded)
                {
                    FailureMessage = "Invalid code";
                    return Page();
                }
                var userUpdate = await _userManager.ResetPasswordAsync(user, token, Password);
                await _signinManager.SignOutAsync();
                if (returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToPage("./Index");
                }
            }
            return Page();
        }
    }
}