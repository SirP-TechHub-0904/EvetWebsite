using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EvetWebsite.Data;
using EvetWebsite.Data.Models;

namespace EvetWebsite.Areas.Admin.Pages.ICommitteeCategory
{
    public class DetailsModel : PageModel
    {
        private readonly EvetWebsite.Data.ApplicationDbContext _context;

        public DetailsModel(EvetWebsite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public CommitteeCategory CommitteeCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CommitteeCategory = await _context.CommitteeCategories.FirstOrDefaultAsync(m => m.Id == id);

            if (CommitteeCategory == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
