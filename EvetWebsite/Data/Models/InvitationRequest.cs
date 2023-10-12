namespace EvetWebsite.Data.Models
{
    public class InvitationRequest
    {
        public long Id { get; set; }
        public string Fullname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Office { get; set; }
        
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public string? Status { get;set; }
    }
}
