using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EvetWebsite.Data;
using EvetWebsite.Data.Models;
using System.Net.Mail;
using EvetWebsite.Services;

namespace EvetWebsite.Pages
{
    public class WriteWishesModel : PageModel
    {
        private readonly EvetWebsite.Data.ApplicationDbContext _context;
        private IEmailSender _email;
        private readonly IWebHostEnvironment _hostingEnv;

        public WriteWishesModel(EvetWebsite.Data.ApplicationDbContext context, IEmailSender email, IWebHostEnvironment hostingEnv)
        {
            _context = context;
            _email = email;
            _hostingEnv = hostingEnv;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public BirthdayMessage BirthdayMessage { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
           BirthdayMessage.Date = DateTime.UtcNow;
            _context.BirthdayMessages.Add(BirthdayMessage);
            await _context.SaveChangesAsync();
            try
            {
                string webRootPath = _hostingEnv.WebRootPath;
                string htmlFilePath = Path.Combine(webRootPath, "Email.html");



                StreamReader sr = new StreamReader(htmlFilePath);
                MailMessage mail = new MailMessage();
                string mi = $"Thank You for the wishes. I really Appreciate.";


                string mailmsg = sr.ReadToEnd();
                mailmsg = mailmsg.Replace("{title}", "Thank You For The Wishes");
                mailmsg = mailmsg.Replace("{name}", BirthdayMessage.Fullname);
                mailmsg = mailmsg.Replace("{linkmessage}", mi);

                mail.Body = mailmsg;
                sr.Close();
                bool res = await _email.SendAsync(mailmsg, BirthdayMessage.Email, "Thank You For The Wishes");
            }
            catch (Exception c)
            {

            }
            TempData["success"] = "Thank You For the Wishes. A so Blessed";
            return RedirectToPage("./Success");
        }
    }
}
