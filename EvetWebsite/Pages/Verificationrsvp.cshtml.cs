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
using Microsoft.AspNetCore.Authorization;

namespace EvetWebsite.Pages
{
    [Authorize]
    public class VerificationrsvpModel : PageModel
    {
        private readonly EvetWebsite.Data.ApplicationDbContext _context;

        public VerificationrsvpModel(EvetWebsite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RSVP RSVP { get; set; }

        public async Task<IActionResult> OnGetAsync(string token)
        {
             

            RSVP = await _context.RSVPs.FirstOrDefaultAsync(m => m.Token == token);

            if (RSVP == null)
            {
                TempData["success"] = "Unable to Verify Guest";
                return RedirectToPage("Success");
            }
            if(RSVP.Present == true)
            {
                TempData["success"] = "Guest Already Verified";
                return RedirectToPage("Success");
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            
            var gst = await _context.RSVPs.FirstOrDefaultAsync(x=>x.Id ==  RSVP.Id);
            gst.Present = true;
            _context.Attach(gst).State = EntityState.Modified;

            
                await _context.SaveChangesAsync();
            TempData["success"] = "Guest Verified";
            return RedirectToPage("/Success");
        }

      
    }
}
