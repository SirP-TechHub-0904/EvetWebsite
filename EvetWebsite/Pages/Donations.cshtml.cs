using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EvetWebsite.Data;
using EvetWebsite.Data.Models;

namespace EvetWebsite.Pages
{
    public class DonationsModel : PageModel
    {
        private readonly EvetWebsite.Data.ApplicationDbContext _context;

        public DonationsModel(EvetWebsite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        
    }
}
