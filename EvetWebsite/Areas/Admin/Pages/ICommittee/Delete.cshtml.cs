using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EvetWebsite.Data;
using EvetWebsite.Data.Models;

namespace EvetWebsite.Areas.Admin.Pages.ICommittee
{
    public class DeleteModel : PageModel
    {
        private readonly EvetWebsite.Data.ApplicationDbContext _context;

        public DeleteModel(EvetWebsite.Data.ApplicationDbContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Committee = await _context.Committees.FindAsync(id);

            if (Committee != null)
            {
                _context.Committees.Remove(Committee);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
