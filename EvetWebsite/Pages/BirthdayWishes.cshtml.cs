﻿using System;
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
    public class BirthdayWishesModel : PageModel
    {
        private readonly EvetWebsite.Data.ApplicationDbContext _context;

        public BirthdayWishesModel(EvetWebsite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<BirthdayMessage> BirthdayMessage { get;set; }

        public async Task OnGetAsync()
        {
            BirthdayMessage = await _context.BirthdayMessages.OrderBy(x=>x.Date).ToListAsync();
        }
    }
}
