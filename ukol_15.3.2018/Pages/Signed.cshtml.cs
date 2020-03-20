using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ukol_15_3_2018.Model;

namespace ukol_15_3_2018
{
    public class SingedModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signinManager;
        private readonly ApplicationDbContext _db;
        protected Note _name;

        [BindProperty]
        public Note Note { get; set; }
        public SingedModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signinManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _signinManager = signinManager;
            _db = db;
        }

        [BindProperty]

        [TempData]
        public string SuccessMessage { get; set; }
        [TempData]
        public string FailureMessage { get; set; }

        public string Author { get { return _name.OwnerId; } }




    }

}