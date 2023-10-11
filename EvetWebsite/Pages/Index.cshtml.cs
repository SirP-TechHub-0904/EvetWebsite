using EvetWebsite.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EvetWebsite.Pages
{
    public class IndexModel : PageModel
    {
        private readonly EvetWebsite.Data.ApplicationDbContext _context;

        public IndexModel(EvetWebsite.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public IList<BirthdayMessage> BirthdayMessage { get; set; }

        public async Task OnGetAsync()
        {
            BirthdayMessage = await _context.BirthdayMessages.Where(x=>x.Disable == false).OrderBy(x => x.Date).Take(4).ToListAsync();

        }
    }
}