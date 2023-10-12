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
    public class AttendingModel : PageModel
    {
        private readonly EvetWebsite.Data.ApplicationDbContext _context;
        private IEmailSender _email;
        private readonly IWebHostEnvironment _hostingEnv;

        public AttendingModel(EvetWebsite.Data.ApplicationDbContext context, IEmailSender email, IWebHostEnvironment hostingEnv)
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
        public InvitationRequest InvitationRequest { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            
            InvitationRequest.Date = DateTime.UtcNow;
            _context.InvitationRequests.Add(InvitationRequest);
            await _context.SaveChangesAsync();
            try
            {
                string webRootPath = _hostingEnv.WebRootPath;
                string htmlFilePath = Path.Combine(webRootPath, "Email.html");



                StreamReader sr = new StreamReader(htmlFilePath);
                MailMessage mail = new MailMessage();
                string mi = $"Thank You for your Interest. An IV will be sent to your email soon";


                string mailmsg = sr.ReadToEnd();
                mailmsg = mailmsg.Replace("{title}", "Thank You for your Interest");
                mailmsg = mailmsg.Replace("{name}", InvitationRequest.Fullname);
                mailmsg = mailmsg.Replace("{linkmessage}", mi);

                mail.Body = mailmsg;
                sr.Close();
                bool res = await _email.SendAsync(mailmsg, InvitationRequest.Email, "Thank You for your Interest");
            }
            catch (Exception c)
            {

            }
            TempData["success"] = "Thank You for your Interest. An IV will be sent to your email soon.";
            return RedirectToPage("./Sucsess");
        }
    }
}
