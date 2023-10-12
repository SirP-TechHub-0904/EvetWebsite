using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EvetWebsite.Data;
using EvetWebsite.Data.Models;
using System.Net.Mail;
using EvetWebsite.Services;
using System.Text.Encodings.Web;

namespace EvetWebsite.Areas.Admin.Pages.IRsvp
{
    public class DetailsModel : PageModel
    {
        private readonly EvetWebsite.Data.ApplicationDbContext _context;
        private IEmailSender _email;
        private readonly IWebHostEnvironment _hostingEnv;

        public DetailsModel(EvetWebsite.Data.ApplicationDbContext context, IEmailSender email, IWebHostEnvironment hostingEnv)
        {
            _context = context;
            _email = email;
            _hostingEnv = hostingEnv;
        }
        [BindProperty]
        public RSVP RSVP { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RSVP = await _context.RSVPs.FirstOrDefaultAsync(m => m.Id == id);

            if (RSVP == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
           var item = await _context.RSVPs.FirstOrDefaultAsync(x=>x.Id==RSVP.Id);
            Zen.Barcode.CodeQrBarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            string userinfo = "";
            try

            { 
                var callbackUrl = Url.Page(
                        "/Comfirm",
                        pageHandler: null,
                        values: new { area = "", token = item.Token },
                        protocol: Request.Scheme);
                try
                {
                    string webRootPath = _hostingEnv.WebRootPath;
                    string htmlFilePath = Path.Combine(webRootPath, "Email.html");



                    StreamReader sr = new StreamReader(htmlFilePath);
                    MailMessage mail = new MailMessage();
                     
                    string base64Image = item.ImageUrl;

                    // Create the HTML img tag with the Base64-encoded image

                    string mi = $"YOUR INVITATION PASS <br><br> <img class=\"\" src=\"data:image/jpg;base64,{base64Image}\" /><br><br>" +
                       $"Please confirm you accept the invite by clicking <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";


                    string mailmsg = sr.ReadToEnd();
                    mailmsg = mailmsg.Replace("{title}", "IV");
                    mailmsg = mailmsg.Replace("{name}", item.Fullname);
                    mailmsg = mailmsg.Replace("{linkmessage}", mi);

                    mail.Body = mailmsg;
                    sr.Close();
                    bool res = await _email.SendAsync(mailmsg, item.Email, "IV");
                }
                catch (Exception c)
                {

                }

                return RedirectToPage("./Details", new { id = item.Id });

            }
            catch (Exception c)
            {
                return Page();
            }



        }

    }
}
