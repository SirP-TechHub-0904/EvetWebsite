using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EvetWebsite.Data;
using EvetWebsite.Data.Models;

namespace EvetWebsite.Areas.Admin.Pages.IReservationType
{
    public class DetailsModel : PageModel
    {
        private readonly EvetWebsite.Data.ApplicationDbContext _context;

        public DetailsModel(EvetWebsite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public ReservationType ReservationType { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ReservationType = await _context.ReservationsType.FirstOrDefaultAsync(m => m.Id == id);

            if (ReservationType == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
