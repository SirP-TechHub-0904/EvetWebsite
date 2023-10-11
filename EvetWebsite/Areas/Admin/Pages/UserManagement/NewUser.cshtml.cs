using EvetWebsite.Data.Models;
using EvetWebsite.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.Encodings.Web;
using IHostingEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;

namespace EvetWebsite.Areas.Admin.Pages.UserManagement
{
    //[Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,Manager")]

    public class NewUserModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<IndexModel> _logger;
        private IEmailSender _email;
        private readonly IWebHostEnvironment _hostingEnv;

        public NewUserModel(SignInManager<ApplicationUser> signInManager,
            ILogger<IndexModel> logger,
            UserManager<ApplicationUser> userManager,
            IEmailSender email,
            IWebHostEnvironment hostingEnv)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _email = email;
            _hostingEnv = hostingEnv;
        }

        [BindProperty]
        public ApplicationUser UserDatas { get; set; }

        [BindProperty]
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get;set; }
        public async Task<IActionResult> OnGetAsync()
        {

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {

            //if (ModelState.IsValid)
            //{
            var user = new ApplicationUser
            {
                UserName = UserDatas.Email,
                Email = UserDatas.Email,
                PhoneNumber = UserDatas.PhoneNumber,
                Fullname = UserDatas.Fullname,
                Date = DateTime.UtcNow,
                Office = UserDatas.Office,
            };


            user.Id = Guid.NewGuid().ToString();
            var result = await _userManager.CreateAsync(user, Password);
            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");
               
                 TempData["success"] = "Successful";
                try
                {
                    string webRootPath = _hostingEnv.WebRootPath;
                    string htmlFilePath = Path.Combine(webRootPath, "Email.html");



                    StreamReader sr = new StreamReader(htmlFilePath);
                    MailMessage mail = new MailMessage();
                    string mi = $"Please confirm your account by <a href=''>clicking here</a>.";


                    string mailmsg = sr.ReadToEnd();
                    mailmsg = mailmsg.Replace("{title}", "Account Activation");
                    mailmsg = mailmsg.Replace("{subtitle}", ""); mailmsg = mailmsg.Replace("{name}", user.Fullname);
                    mailmsg = mailmsg.Replace("{linkmessage}", mi);

                    mail.Body = mailmsg;
                    sr.Close();

                }
                catch (Exception c)
                {

                }
                return RedirectToPage("./Details", new {id = user.Id });

            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            //}

            return Page();
        }

    }
}
