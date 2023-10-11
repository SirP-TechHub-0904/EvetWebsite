using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EvetWebsite.Data;
using EvetWebsite.Data.Models;

namespace EvetWebsite.Areas.Admin.Pages.ICommittee
{
    public class CreateModel : PageModel
    {
        private readonly EvetWebsite.Data.ApplicationDbContext _context;

        public CreateModel(EvetWebsite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CommitteeCategoryId"] = new SelectList(_context.CommitteeCategories, "Id", "Title");
            return Page();
        }

        [BindProperty]
        public Committee Committee { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            

            _context.Committees.Add(Committee);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
