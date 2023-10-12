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
    public class CommitteesModel : PageModel
    {
        private readonly EvetWebsite.Data.ApplicationDbContext _context;

        public CommitteesModel(EvetWebsite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<CommitteeCategory> CommitteeCategory { get;set; }

        public async Task OnGetAsync()
        {
            CommitteeCategory = await _context.CommitteeCategories.Include(x=>x.Committees).ToListAsync();
        }
    }
}
