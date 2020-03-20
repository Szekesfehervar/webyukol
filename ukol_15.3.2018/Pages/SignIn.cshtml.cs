using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ukol_15_3_2018.Model;

namespace ukol_15._3._2018
{
    public class SignInModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signinManager;

        public SignInModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signinManager)
        {
            _userManager = userManager;
            _signinManager = signinManager;
            Input = new LoginInputModel();
        }

        [BindProperty]
        public LoginInputModel Input { get; set; }

        [TempData]
        public string SuccessMessage { get; set; }
        [TempData]
        public string FailureMessage { get; set; }

        public async Task OnGet(string returnUrl = null)
        {
            returnUrl ??= Url.Content("./Signed");
            await HttpContext.SignOutAsync();
            Input.ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("./Signed");
            if (ModelState.IsValid)
            {
                var result = await _signinManager.PasswordSignInAsync(Input.UserName, Input.Password, Input.RememberMe, false);
                if (result.Succeeded)
                {
                    Response.Redirect("./Signed");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Sign In Failed");
                    return Page();
                }
            }
            return Page();
        }
    }
    public class LoginInputModel
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }
}