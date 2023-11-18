using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EvetWebsite.Data;
using EvetWebsite.Data.Models;

namespace EvetWebsite.Areas.Admin.Pages.PreorderPage
{
    public class DetailsModel : PageModel
    {
        private readonly EvetWebsite.Data.ApplicationDbContext _context;

        public DetailsModel(EvetWebsite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public PreorderForm PreorderForm { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PreorderForm = await _context.PreorderForms.FirstOrDefaultAsync(m => m.Id == id);

            if (PreorderForm == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
