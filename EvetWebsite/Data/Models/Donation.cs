namespace EvetWebsite.Data.Models
{
    public class Donation
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Office { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public string ApprovedBy { get; set; }
        public Status Status { get; set; }
        public PaymentType PaymentType { get; set; }
    }
}
