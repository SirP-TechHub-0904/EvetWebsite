using EvetWebsite.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EvetWebsite.Areas.Admin.Pages.UserManagement
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,Manager")]

    public class EditModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<IndexModel> _logger;

        public EditModel(SignInManager<ApplicationUser> signInManager,
            ILogger<IndexModel> logger,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }
        [BindProperty]
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
             
            try
            {
                var userupdate = await _userManager.FindByIdAsync(UserDatas.Id);
                
                userupdate.Fullname = UserDatas.Fullname; 
                userupdate.PhoneNumber = UserDatas.PhoneNumber; 
                userupdate.UserStatus = UserDatas.UserStatus;
                userupdate.Office = UserDatas.Office;

                await _userManager.UpdateAsync(userupdate);
            }
            catch (DbUpdateConcurrencyException)
            {
                
                    throw;
                
            }

            return RedirectToPage("./Index");
        }

    }
}
