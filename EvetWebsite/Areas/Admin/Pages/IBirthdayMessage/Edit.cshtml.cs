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

namespace EvetWebsite.Areas.Admin.Pages.IBirthdayMessage
{
    public class EditModel : PageModel
    {
        private readonly EvetWebsite.Data.ApplicationDbContext _context;

        public EditModel(EvetWebsite.Data.ApplicationDbContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(BirthdayMessage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BirthdayMessageExists(BirthdayMessage.Id))
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

        private bool BirthdayMessageExists(long id)
        {
            return _context.BirthdayMessages.Any(e => e.Id == id);
        }
    }
}
