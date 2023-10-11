using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EvetWebsite.Data;
using EvetWebsite.Data.Models;

namespace EvetWebsite.Pages
{
    public class ReserveModel : PageModel
    {
        private readonly EvetWebsite.Data.ApplicationDbContext _context;

        public ReserveModel(EvetWebsite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(long id, string hotel)
        {
        ViewData["ReservationTypeId"] = id;
            return Page();
        }

        [BindProperty]
        public Reservation Reservation { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Reservations.Add(Reservation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
