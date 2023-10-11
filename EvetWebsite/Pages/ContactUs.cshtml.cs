using EvetWebsite.Data.Models;
using EvetWebsite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace EvetWebsite.Pages
{
    public class ContactUsModel : PageModel
    {
        private readonly EvetWebsite.Data.ApplicationDbContext _context;
        private IEmailSender _email;
        private readonly IWebHostEnvironment _hostingEnv;

        public ContactUsModel(EvetWebsite.Data.ApplicationDbContext context, IEmailSender email, IWebHostEnvironment hostingEnv)
        {
            _context = context;
            _email = email;
            _hostingEnv = hostingEnv;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            
            return Page();
        }

        [BindProperty]
        public Contact Contact { get; set; }
         // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            Contact.Date = DateTime.UtcNow;
            _context.Contacts.Add(Contact);
            await _context.SaveChangesAsync();
            TempData["success"] = "Thank You For Reaching out to us. We will Get Back to You Shortly";
            try
            {
                string webRootPath = _hostingEnv.WebRootPath;
                string htmlFilePath = Path.Combine(webRootPath, "Email.html");



                StreamReader sr = new StreamReader(htmlFilePath);
                MailMessage mail = new MailMessage();
                string mi = $"Thank You For Reaching out to us. We will Get Back to You Shortly";


                string mailmsg = sr.ReadToEnd();
                mailmsg = mailmsg.Replace("{title}", "Contact Us Feedback Paramallam @60");
                mailmsg = mailmsg.Replace("{name}", Contact.Name);
                mailmsg = mailmsg.Replace("{linkmessage}", mi);

                mail.Body = mailmsg;
                sr.Close();
                bool res = await _email.SendAsync(mailmsg, Contact.Email, "Contact Us Feedback Paramallam @60");
            }
            catch (Exception c)
            {

            }
            return RedirectToPage("./Success");
        }
    }
}
