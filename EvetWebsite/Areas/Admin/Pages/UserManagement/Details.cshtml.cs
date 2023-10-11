using EvetWebsite.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EvetWebsite.Areas.Admin.Pages.UserManagement
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,Manager,Admin")]

    public class DetailsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<DetailsModel> _logger; 

        public DetailsModel(SignInManager<ApplicationUser> signInManager,
            ILogger<DetailsModel> logger,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger; 
        }
        public ApplicationUser UserDatas { get; set; } 

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserDatas = await _userManager.FindByIdAsync(id);

            if (UserDatas == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
