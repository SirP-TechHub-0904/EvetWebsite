using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EvetWebsite.Data;
using EvetWebsite.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using EvetWebsite.Services;

namespace EvetWebsite.Pages
{
    public class MakeReservationModel : PageModel
    {
        private readonly EvetWebsite.Data.ApplicationDbContext _context;
        private IEmailSender _email;
        private readonly IWebHostEnvironment _hostingEnv;

        public MakeReservationModel(EvetWebsite.Data.ApplicationDbContext context, IEmailSender email, IWebHostEnvironment hostingEnv)
        {
            _context = context;
            _email = email;
            _hostingEnv = hostingEnv;
        }
        public async Task<IActionResult> OnGetAsync(long id, string hotel)
        {
            ReservationType = await _context.ReservationsType.FirstOrDefaultAsync(x=>x.Id == id);
            if(ReservationType == null)
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }

        [BindProperty]
        public Reservation Reservation { get; set; }
        [BindProperty]
        public ReservationType ReservationType { get; set; }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
           
            Reservation.Date = DateTime.UtcNow;
            _context.Reservations.Add(Reservation);
            await _context.SaveChangesAsync();
            TempData["success"] = "Your Reservation has be placed. You will receive a call soon";
            try
            {
                string webRootPath = _hostingEnv.WebRootPath;
                string htmlFilePath = Path.Combine(webRootPath, "Email.html");



                StreamReader sr = new StreamReader(htmlFilePath);
                MailMessage mail = new MailMessage();
                string mi = $"Thank You for making a reservation. We will Keep you Posted.";


                string mailmsg = sr.ReadToEnd();
                mailmsg = mailmsg.Replace("{title}", "Hotel Reservation Paramallam @60");
                mailmsg = mailmsg.Replace("{name}", Reservation.Fullname);
                mailmsg = mailmsg.Replace("{linkmessage}", mi);

                mail.Body = mailmsg;
                sr.Close();
                bool res = await _email.SendAsync(mailmsg, Reservation.Email, "Hotel Reservation Paramallam @60");
            }
            catch (Exception c)
            {

            }
            return RedirectToPage("./Success");
        }
    }
}
