﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ukol_15_3_2018.Model;

namespace ukol_15_3_2018
{
    public class SignOutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public SignOutModel(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGetAsync(string returnUrl)
        {
            await _signInManager.SignOutAsync();
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage("./Index");
            }
        }
    }
}