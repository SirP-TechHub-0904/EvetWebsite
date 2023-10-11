using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EvetWebsite.Data;
using EvetWebsite.Data.Models;

namespace EvetWebsite.Areas.Admin.Pages.ICommitteeCategory
{
    public class DeleteModel : PageModel
    {
        private readonly EvetWebsite.Data.ApplicationDbContext _context;

        public DeleteModel(EvetWebsite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CommitteeCategory CommitteeCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CommitteeCategory = await _context.CommitteeCategories.FirstOrDefaultAsync(m => m.Id == id);

            if (CommitteeCategory == null)
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

            CommitteeCategory = await _context.CommitteeCategories.FindAsync(id);

            if (CommitteeCategory != null)
            {
                _context.CommitteeCategories.Remove(CommitteeCategory);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
