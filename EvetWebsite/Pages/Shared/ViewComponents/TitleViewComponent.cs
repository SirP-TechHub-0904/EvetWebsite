using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EvetWebsite.Pages.Shared.ViewComponents
{
    
    public class TitleViewComponent : ViewComponent
    {

        private readonly RoleManager<IdentityRole> _roleManager;

        public TitleViewComponent(
            RoleManager<IdentityRole> roleManager
            )
        {

            _roleManager = roleManager;
        }

        public IdentityRole RoleData { get; set; }

        public async Task<IViewComponentResult> InvokeAsync(string name)
        {
            if (name.Contains("Pre Order"))
            {
                TempData["fullname"] = "pppppp";
            }
            else
            {
                TempData["fullname"] = "Home";
            }
            
            return View( );
        }
    }
}
