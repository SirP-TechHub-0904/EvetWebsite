using EvetWebsite.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EvetWebsite.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<BirthdayMessage> BirthdayMessages { get; set; }
        public DbSet<Committee> Committees { get; set; }
        public DbSet<CommitteeCategory> CommitteeCategories { get; set; }
        public DbSet<Donation> Donations { get; set; }  
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationType> ReservationsType { get; set;}
        public DbSet<Contact> Contacts { get; set; }
    }
}