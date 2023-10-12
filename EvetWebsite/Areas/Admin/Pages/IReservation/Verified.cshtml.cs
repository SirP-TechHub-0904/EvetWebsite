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
using EvetWebsite.Services.AWS;
using EvetWebsite.Services.AwsDtos;
using System.Net.Mail;
using EvetWebsite.Services;

namespace EvetWebsite.Areas.Admin.Pages.IReservation
{
    public class VerifiedModel : PageModel
    {
        private readonly EvetWebsite.Data.ApplicationDbContext _context;
        private readonly IConfiguration _config;
        private readonly IStorageService _storageService;
        private readonly IWebHostEnvironment _hostingEnv;
        private IEmailSender _email;

        public VerifiedModel(EvetWebsite.Data.ApplicationDbContext context, IConfiguration config, IStorageService storageService, IWebHostEnvironment hostingEnv, IEmailSender email)
        {
            _context = context;
            _config = config;
            _storageService = storageService;
            _hostingEnv = hostingEnv;
            _email = email;
        }

        [BindProperty]
        public Reservation Reservation { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Reservation = await _context.Reservations
                .Include(r => r.ReservationType).FirstOrDefaultAsync(m => m.Id == id);

            if (Reservation == null)
            {
                return NotFound();
            }
            ViewData["ReservationTypeId"] = new SelectList(_context.ReservationsType, "Id", "Name");
            return Page();
        }
        [BindProperty]
        public IFormFile? imagefile { get; set; }
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            var geti = await _context.Reservations.FirstOrDefaultAsync(x => x.Id == Reservation.Id);
            geti.ReservationNote = Reservation.ReservationNote;
            geti.VerifiedReservation = Reservation.VerifiedReservation;

            _context.Attach(geti).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            try
            {
                string webRootPath = _hostingEnv.WebRootPath;
                string htmlFilePath = Path.Combine(webRootPath, "Email.html");



                StreamReader sr = new StreamReader(htmlFilePath);
                MailMessage mail = new MailMessage();
                string mi = geti.ReservationNote;


                string mailmsg = sr.ReadToEnd();
                mailmsg = mailmsg.Replace("{title}", "Reservation Response");
                mailmsg = mailmsg.Replace("{name}", geti.Fullname);
                mailmsg = mailmsg.Replace("{linkmessage}", mi);

                mail.Body = mailmsg;
                sr.Close();
                bool res = await _email.SendAsync(mailmsg, geti.Email, "Reservation Response");
            }
            catch (Exception c)
            {

            }
            TempData["success"] = "Reservation Feedback Sent";
            return RedirectToPage("./Details", new { id = geti.Id});
        }

        private bool ReservationExists(long id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
