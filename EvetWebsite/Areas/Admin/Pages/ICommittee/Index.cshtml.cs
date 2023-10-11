using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EvetWebsite.Data;
using EvetWebsite.Data.Models;

namespace EvetWebsite.Areas.Admin.Pages.ICommittee
{
    public class IndexModel : PageModel
    {
        private readonly EvetWebsite.Data.ApplicationDbContext _context;

        public IndexModel(EvetWebsite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Committee> Committee { get;set; }

        public async Task OnGetAsync()
        {
            Committee = await _context.Committees
                .Include(c => c.CommitteeCategory).ToListAsync();
        }
    }
}
