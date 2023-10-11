using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EvetWebsite.Data;
using EvetWebsite.Data.Models;

namespace EvetWebsite.Pages
{
    public class ReservationPageModel : PageModel
    {
        private readonly EvetWebsite.Data.ApplicationDbContext _context;

        public ReservationPageModel(EvetWebsite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ReservationType> ReservationType { get;set; }

        public async Task OnGetAsync()
        {
            ReservationType = await _context.ReservationsType.ToListAsync();
        }
    }
}
