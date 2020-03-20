using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ukol_15_3_2018
{
    [Authorize(Roles = "Administrátor")]
    public class AdminModel : PageModel
    {
        public void OnGet()
        {

        }
    }
}