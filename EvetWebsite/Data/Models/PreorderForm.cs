using System.ComponentModel.DataAnnotations;

namespace EvetWebsite.Data.Models
{
    public class PreorderForm
    {
        public long Id { get; set; }

        [Display(Name ="Email Address")]
        public string? Email { get; set; }

        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Street Address")]
        public string? StreetAddress { get; set; }

        [Display(Name = "City")]
        public string? City { get; set; }

        [Display(Name = "State")]
        public string? State { get; set; }


        [Display(Name = "Zip Code")]
        public string? ZipCode { get; set; }

        [Display(Name = "Country")]
        public string? Country { get; set; }

        [Display(Name = "Quantity")]
        public string? Quantity { get; set; }

      

    }
}
