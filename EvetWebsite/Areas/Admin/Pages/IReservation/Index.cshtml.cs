using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EvetWebsite.Data;
using EvetWebsite.Data.Models;

namespace EvetWebsite.Areas.Admin.Pages.IReservation
{
    public class IndexModel : PageModel
    {
        private readonly EvetWebsite.Data.ApplicationDbContext _context;

        public IndexModel(EvetWebsite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Reservation> Reservation { get;set; }

        public async Task OnGetAsync()
        {
            Reservation = await _context.Reservations
                .Include(r => r.ReservationType).ToListAsync();
        }
    }
}
