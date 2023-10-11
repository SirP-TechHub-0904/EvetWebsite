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

namespace EvetWebsite.Areas.Admin.Pages.IReservationType
{
    public class EditModel : PageModel
    {
        private readonly EvetWebsite.Data.ApplicationDbContext _context;
        private readonly IConfiguration _config;
        private readonly IStorageService _storageService;
        public EditModel(EvetWebsite.Data.ApplicationDbContext context, IConfiguration config, IStorageService storageService)
        {
            _context = context;
            _config = config;
            _storageService = storageService;
        }

        [BindProperty]
        public IFormFile? imagefile { get; set; }
        [BindProperty]
        public ReservationType ReservationType { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ReservationType = await _context.ReservationsType.FirstOrDefaultAsync(m => m.Id == id);

            if (ReservationType == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //image
            if (imagefile != null)
            {
                try
                {
                    // Process file
                    await using var memoryStream = new MemoryStream();
                    await imagefile.CopyToAsync(memoryStream);

                    var fileExt = Path.GetExtension(imagefile.FileName);
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

                    var xresult = await _storageService.UploadFileReturnUrlAsync(s3Obj, cred, ReservationType.ImageKey);
                    // 
                    if (xresult.Message.Contains("200"))
                    {
                        ReservationType.ImageUrl = xresult.Url;
                        ReservationType.ImageKey = xresult.Key;
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
            }

            _context.Attach(ReservationType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationTypeExists(ReservationType.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ReservationTypeExists(long id)
        {
            return _context.ReservationsType.Any(e => e.Id == id);
        }
    }
}
