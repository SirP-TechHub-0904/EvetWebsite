using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EvetWebsite.Data;
using EvetWebsite.Data.Models;

namespace EvetWebsite.Areas.Admin.Pages.IBirthdayMessage
{
    public class DeleteModel : PageModel
    {
        private readonly EvetWebsite.Data.ApplicationDbContext _context;

        public DeleteModel(EvetWebsite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BirthdayMessage BirthdayMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BirthdayMessage = await _context.BirthdayMessages.FirstOrDefaultAsync(m => m.Id == id);

            if (BirthdayMessage == null)
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

            BirthdayMessage = await _context.BirthdayMessages.FindAsync(id);

            if (BirthdayMessage != null)
            {
                _context.BirthdayMessages.Remove(BirthdayMessage);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
