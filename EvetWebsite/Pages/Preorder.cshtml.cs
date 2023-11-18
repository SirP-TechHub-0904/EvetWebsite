using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EvetWebsite.Data;
using EvetWebsite.Data.Models;
using EvetWebsite.Services;
using System.Net.Mail;

namespace EvetWebsite.Pages
{
    public class PreorderModel : PageModel
    {
        private readonly EvetWebsite.Data.ApplicationDbContext _context;
        private IEmailSender _email;
        private readonly IWebHostEnvironment _hostingEnv;

        public PreorderModel(EvetWebsite.Data.ApplicationDbContext context, IEmailSender email, IWebHostEnvironment hostingEnv)
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
        public PreorderForm PreorderForm { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.PreorderForms.Add(PreorderForm);
            await _context.SaveChangesAsync();
            TempData["success"] = "Thank You for the Pre Order on Gender and Development in Nigeria: Concepts, Issues and Strategies by Prof. O.J Para-Mallam, mni";

            try
            {
                string webRootPath = _hostingEnv.WebRootPath;
                string htmlFilePath = Path.Combine(webRootPath, "Email.html");



                StreamReader sr = new StreamReader(htmlFilePath);
                MailMessage mail = new MailMessage();
                string mi = $"Thank You for the Pre Order on Gender and Development in Nigeria: Concepts, Issues and Strategies by Prof. O.J Para-Mallam, mni";
                string mailmsg = sr.ReadToEnd();
                mailmsg = mailmsg.Replace("{title}", "Pre Order Form");
                mailmsg = mailmsg.Replace("{name}", PreorderForm.FirstName + " "+ PreorderForm.LastName);
                mailmsg = mailmsg.Replace("{linkmessage}", mi);

                mail.Body = mailmsg;
                sr.Close();
                bool res = await _email.SendAsync(mailmsg, PreorderForm.Email, "Pre Order on Gender and Development in Nigeria by Prof. O.J Para-Mallam, mni");
            }
            catch (Exception c)
            {

            }
            return RedirectToPage("./Success"); 
        }
    }
}
