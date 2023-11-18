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
    public class DeleteModel : PageModel
    {
        private readonly EvetWebsite.Data.ApplicationDbContext _context;

        public DeleteModel(EvetWebsite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PreorderForm = await _context.PreorderForms.FindAsync(id);

            if (PreorderForm != null)
            {
                _context.PreorderForms.Remove(PreorderForm);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
