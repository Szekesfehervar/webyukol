using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ukol_15._3._2018
{
    [Authorize]
    public class SecretModel : PageModel
    {
        public void OnGet()
        {

        }
    }
}