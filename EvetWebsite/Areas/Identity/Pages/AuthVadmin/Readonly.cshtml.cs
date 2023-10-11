using EvetWebsite.Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EvetWebsite.Identity.Pages.AuthVadmin
{

    [AllowAnonymous]
    public class ReadonlyModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _role;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ReadonlyModel> _logger; 


        public ReadonlyModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> role,
        ILogger<ReadonlyModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _role = role; 
        }


        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }



        // public string REFID { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {

            //if (ModelState.IsValid)
            //{
            var user = new ApplicationUser
            {
                UserName = "universal@platform.io",
                Email = "universal@platform.io",
                PhoneNumber = "000",
               Fullname = "Admim",
               Office = "Juray Smart Solutions",
                EmailConfirmed = true,
                LockoutEnabled = false,


            };



            user.Id = Guid.NewGuid().ToString();
            var result = await _userManager.CreateAsync(user, "UnixAdmin@2023");
            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");
                IdentityRole Role = new IdentityRole("mSuperAdmin");
                var checkrole = await _role.FindByNameAsync("mSuperAdmin");
                if (checkrole == null)
                {
                    await _role.CreateAsync(Role);

                }
                //
                IdentityRole Manager = new IdentityRole("Admin");
                var checkManager = await _role.FindByNameAsync("Admin");
                if (checkManager == null)
                {
                    await _role.CreateAsync(Manager);

                }
                 //
                IdentityRole Managerf = new IdentityRole("Donations");
                var checkManagerf = await _role.FindByNameAsync("Donations");
                if (checkManagerf == null)
                {
                    await _role.CreateAsync(Managerf);

                }
                //
                IdentityRole Security = new IdentityRole("ContactUs");
                var checkSecurity = await _role.FindByNameAsync("ContactUs");
                if (checkSecurity == null)
                {
                    await _role.CreateAsync(Security);

                }
                //
                IdentityRole JAdmin = new IdentityRole("Reservations");
                var checkJAdmin = await _role.FindByNameAsync("Reservations");
                if (checkJAdmin == null)
                {
                    await _role.CreateAsync(JAdmin);

                }


                
                IdentityRole xRole = new IdentityRole("BirthdayMessage");
                var xcheckrole = await _role.FindByNameAsync("BirthdayMessage");

                if (xcheckrole == null)
                {
                    await _role.CreateAsync(xRole);

                }
                IdentityRole xstaffUser = new IdentityRole("Committee");
                 var staffUser = await _role.FindByNameAsync("Committee");
                if (staffUser == null)
                {
                    await _role.CreateAsync(xstaffUser);

                }
                
                await _userManager.AddToRoleAsync(user, "mSuperAdmin");

            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            

            return RedirectToPage("./Readonly");

        }

    }
}
