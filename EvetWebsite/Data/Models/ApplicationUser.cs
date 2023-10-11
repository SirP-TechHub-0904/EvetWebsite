using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EvetWebsite.Data.Models
{
    public class ApplicationUser : IdentityUser<string>
    {
        public string Fullname { get; set; }
        public string Office { get; set; }
        public DateTime Date { get; set; }
        [Display(Name = "User Status")]
        public UserStatus UserStatus { get; set; }
    }
}
