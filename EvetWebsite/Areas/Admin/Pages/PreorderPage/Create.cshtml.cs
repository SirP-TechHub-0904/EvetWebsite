using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EvetWebsite.Data;
using EvetWebsite.Data.Models;

namespace EvetWebsite.Areas.Admin.Pages.PreorderPage
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
            return Page();
        }

        [BindProperty]
        public PreorderForm PreorderForm { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.PreorderForms.Add(PreorderForm);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
