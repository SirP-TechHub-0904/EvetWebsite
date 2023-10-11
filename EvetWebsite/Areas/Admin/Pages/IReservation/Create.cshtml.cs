using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EvetWebsite.Data;
using EvetWebsite.Data.Models;
using EvetWebsite.Services.AWS;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Metadata;
using EvetWebsite.Services.AwsDtos;

namespace EvetWebsite.Areas.Admin.Pages.IReservation
{
    public class CreateModel : PageModel
    {
        private readonly EvetWebsite.Data.ApplicationDbContext _context;
        private readonly IConfiguration _config;
        private readonly IStorageService _storageService;
        public CreateModel(EvetWebsite.Data.ApplicationDbContext context, IConfiguration config, IStorageService storageService)
        {
            _context = context;
            _config = config;
            _storageService = storageService;
        }

        [BindProperty]
        public IFormFile? imagefile { get; set; }
        public IActionResult OnGet()
        {
        ViewData["ReservationTypeId"] = new SelectList(_context.ReservationsType, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Reservation Reservation { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          

            _context.Reservations.Add(Reservation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
