using EvetWebsite.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EvetWebsite.Areas.Admin.Pages.Dashboard
{
    public class IndexModel : PageModel
    {
        private readonly EvetWebsite.Data.ApplicationDbContext _context;

        public IndexModel(EvetWebsite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

 
        public int Messages { get;set; }
        public int Committees { get;set; }
        public int Contact { get;set; }
        public decimal Donations { get;set; }
        public int Reservation { get;set; } 
        public int RSVP { get;set; }
        public int Present { get;set; } 
        public int Request { get;set; }

        public async Task OnGetAsync()
        {
            Messages = await _context.BirthdayMessages.CountAsync();
            Committees = await _context.CommitteeCategories.CountAsync();
            Contact = await _context.Contacts.CountAsync();
            Donations = await _context.Donations.Where(x=>x.Status == Status.Completed).SumAsync(x=>x.Amount);
            Present = await _context.RSVPs.Where(x=>x.Present == true).CountAsync();
            Reservation = await _context.Reservations.CountAsync();
            RSVP = await _context.RSVPs.CountAsync();
            Request = await _context.InvitationRequests.CountAsync();
        }
    }
}
