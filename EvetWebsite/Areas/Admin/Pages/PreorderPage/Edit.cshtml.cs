using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EvetWebsite.Data;
using EvetWebsite.Data.Models;

namespace EvetWebsite.Areas.Admin.Pages.PreorderPage
{
    public class EditModel : PageModel
    {
        private readonly EvetWebsite.Data.ApplicationDbContext _context;

        public EditModel(EvetWebsite.Data.ApplicationDbContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(PreorderForm).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PreorderFormExists(PreorderForm.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PreorderFormExists(long id)
        {
            return _context.PreorderForms.Any(e => e.Id == id);
        }
    }
}
