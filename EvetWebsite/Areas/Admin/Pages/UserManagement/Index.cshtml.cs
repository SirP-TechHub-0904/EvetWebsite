using EvetWebsite.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EvetWebsite.Areas.Admin.Pages.UserManagement
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,Manager,Admin")]
    public class RoleDTO
    {
        public string RoleName { get; set; }
        // You can add more properties related to roles if needed
    }
    public class ProfileDto
    {
        public string Id { get; set; }
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public UserStatus Status { get; set; }

        public string Roles { get; set; }
    }
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<IndexModel> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IndexModel(SignInManager<ApplicationUser> signInManager,
            ILogger<IndexModel> logger,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _roleManager = roleManager;
        }
        public IList<ProfileDto> UserDatasList { get; set; }

        public async Task OnGetAsync()
        {
            UserDatasList = new List<ProfileDto>();
            var UserDatas = await _userManager.Users.ToListAsync();

            var excludedRoles = new List<string> { "mSuperAdmin", "SubAdmin" };

            foreach (var user in UserDatas)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                if (userRoles.All(roleName => !excludedRoles.Contains(roleName)))
                {
                    var profile = new ProfileDto
                    {
                        Fullname = user.Fullname,
                        Phone = user.PhoneNumber,
                        Email = user.Email,
                        Status = user.UserStatus,
                        Id = user.Id,
                        Roles = string.Join(", ", userRoles) // Combine roles into a comma-separated string
                    };

                    UserDatasList.Add(profile);
                }
            }
        }
    }
}
