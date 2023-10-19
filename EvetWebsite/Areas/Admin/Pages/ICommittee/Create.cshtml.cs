using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EvetWebsite.Data;
using EvetWebsite.Data.Models;
using EvetWebsite.Services.AwsDtos;
using EvetWebsite.Services.AWS;

namespace EvetWebsite.Areas.Admin.Pages.ICommittee
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
        ViewData["CommitteeCategoryId"] = new SelectList(_context.CommitteeCategories, "Id", "Title");
            return Page();
        }

        [BindProperty]
        public Committee Committee { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
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

                    var xresult = await _storageService.UploadFileReturnUrlAsync(s3Obj, cred, "");
                    // 
                    if (xresult.Message.Contains("200"))
                    {
                        Committee.ImageUrl = xresult.Url;
                        Committee.ImageKey = xresult.Key;
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

            _context.Committees.Add(Committee);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
