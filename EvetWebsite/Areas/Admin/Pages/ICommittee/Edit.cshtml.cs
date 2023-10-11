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

namespace EvetWebsite.Areas.Admin.Pages.ICommittee
{
    public class EditModel : PageModel
    {
        private readonly EvetWebsite.Data.ApplicationDbContext _context;

        public EditModel(EvetWebsite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Committee Committee { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Committee = await _context.Committees
                .Include(c => c.CommitteeCategory).FirstOrDefaultAsync(m => m.Id == id);

            if (Committee == null)
            {
                return NotFound();
            }
           ViewData["CommitteeCategoryId"] = new SelectList(_context.CommitteeCategories, "Id", "Title");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
           

            _context.Attach(Committee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommitteeExists(Committee.Id))
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

        private bool CommitteeExists(long id)
        {
            return _context.Committees.Any(e => e.Id == id);
        }
    }
}
