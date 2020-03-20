using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Manage.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ukol_15_3_2018.Model;
using ukol_15_3_2018.Services;

namespace ukol_15_3_2018
{
    public class PasswordReset : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly EmailSender _emailSender;
        public PasswordReset(UserManager<ApplicationUser> userManager, EmailSender emailsender)
        {
            _userManager = userManager;
            _emailSender = emailsender;
        }
        [TempData]
        public string SuccessMessage { get; set; }
        [TempData]
        public string FailureMessage { get; set; }
        public string ReturnUrl { get; set; }
        [BindProperty]
        public string Email { get; set; }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("./");
            if (ModelState.IsValid)
            {
                if (Email != null)
                {
                    var user = await _userManager.FindByEmailAsync(Email);
                    if (user != null)
                    {
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        await _emailSender.SendEmailAsync(Email, "Zdary, reset hesla", code);
                        SuccessMessage = "kod mas na mailu";
                        return RedirectToPage("NewPassword", new { email = Email });
                    }
                }
            }
            return Page();
        }
    }
}