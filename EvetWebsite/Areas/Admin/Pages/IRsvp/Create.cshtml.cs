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
using System.Text.Encodings.Web;
using EvetWebsite.Services.AWS;
using EvetWebsite.Services.AwsDtos;

namespace EvetWebsite.Areas.Admin.Pages.IRsvp
{
    public class CreateModel : PageModel
    {
        private readonly EvetWebsite.Data.ApplicationDbContext _context;
        private IEmailSender _email;
        private readonly IWebHostEnvironment _hostingEnv;
        private readonly IConfiguration _config;
        private readonly IStorageService _storageService;
        public CreateModel(EvetWebsite.Data.ApplicationDbContext context, IEmailSender email, IWebHostEnvironment hostingEnv, IConfiguration config, IStorageService storageService)
        {
            _context = context;
            _email = email;
            _hostingEnv = hostingEnv;
            _config = config;
            _storageService = storageService;
        }


        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public RSVP RSVP { get; set; }
        [BindProperty]
        public byte[] Image { get; set; }
        [BindProperty]
        public byte[] JurayImage { get; set; }
        private byte[] turnImageToByteArray(System.Drawing.Image img)
        {
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            return ms.ToArray();
        }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            RSVP.Token = Guid.NewGuid().ToString();
            RSVP.Date = DateTime.UtcNow;
            Zen.Barcode.CodeQrBarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            string userinfo = "";
            try

            {
                string link = "https://www.paramallam.com.ng/verificationrsvp/" + RSVP.Token;
                System.Drawing.Image img = barcode.Draw(link, 100);

                byte[] imgBytes = turnImageToByteArray(img);
                //
                try
                {
                    // Process file
                    await using var memoryStream = new MemoryStream();
                    await memoryStream.WriteAsync(imgBytes, 0, imgBytes.Length); // Write the byte array to the MemoryStream
                     
                    var fileExt = Path.GetExtension(RSVP.Token);
                    var docName = $"{Guid.NewGuid()}{fileExt}";
                    // call server

                    var s3Obj = new Services.AwsDtos.S3Object()
                    {
                        BucketName = "juray2023",
                        InputStream = memoryStream,
                        Name = docName
                    };

                    var cred = new AwsCredentials()
                    {
                        AccessKey = _config["AwsConfiguration:AWSAccessKey"],
                        SecretKey = _config["AwsConfiguration:AWSSecretKey"]
                    };

                    var xresult = await _storageService.UploadFileReturnUrlAsync(s3Obj, cred, "");
                    // 
                    if (xresult.Message.Contains("200"))
                    {
                        RSVP.ImageUrl = xresult.Url;
                        RSVP.ImageKey = xresult.Key;
                    }
                    else
                    {
                        TempData["error"] = "unable to upload image";
                        //return Page();
                    }
                }
                catch (Exception c)
                {

                } 
                RSVP.SentViaEmail = true;
                _context.RSVPs.Add(RSVP);
                await _context.SaveChangesAsync();
                TempData["success"] = "RSVP Sent Successfully";
                var callbackUrl = Url.Page(
                        "/Comfirm",
                        pageHandler: null,
                        values: new { area = "", token = RSVP.Token },
                        protocol: Request.Scheme);
                try
                {
                    string webRootPath = _hostingEnv.WebRootPath;
                    string htmlFilePath = Path.Combine(webRootPath, "Email.html");

                    // Convert the byte array to a Base64-encoded string
                    string base64Image = RSVP.ImageUrl;

                    StreamReader sr = new StreamReader(htmlFilePath);
                    MailMessage mail = new MailMessage();
                    string mi = $"YOUR INVITATION PASS <br><br> <img class=\"\" src=\"{base64Image}\" /><br><br>" +
                       $"Please confirm you accept the invite by clicking <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";

                    string mailmsg = sr.ReadToEnd();
                    mailmsg = mailmsg.Replace("{title}", "IV");
                    mailmsg = mailmsg.Replace("{name}", RSVP.Fullname);
                    mailmsg = mailmsg.Replace("{linkmessage}", mi);

                    mail.Body = mailmsg;
                    sr.Close();
                    bool res = await _email.SendAsync(mailmsg, RSVP.Email, "IV");
                }
                catch (Exception c)
                {

                }

                return RedirectToPage("./Details", new { id = RSVP.Id });

            }
            catch (Exception c)
            {
                return Page();
            }


            
        }
    }
}
