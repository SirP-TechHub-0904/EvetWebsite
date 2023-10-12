using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EvetWebsite.Data;
using EvetWebsite.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace EvetWebsite.Pages
{
     public class ComfirmModel : PageModel
    {
        private readonly EvetWebsite.Data.ApplicationDbContext _context;

        public ComfirmModel(EvetWebsite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public RSVP RSVP { get; set; }

        public async Task<IActionResult> OnGetAsync(string token)
        {


            RSVP = await _context.RSVPs.FirstOrDefaultAsync(m => m.Token == token);

            if (RSVP == null)
            {
                return NotFound();
            }
            RSVP.IVResponse = true;
            _context.Attach(RSVP).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            TempData["success"] = "Invitation Response Comfirmed";
            return RedirectToPage("/Success");
        }

     }
}
