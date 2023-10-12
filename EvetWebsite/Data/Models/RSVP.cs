namespace EvetWebsite.Data.Models
{
    public class RSVP
    {
        public long Id { get; set; }
        public string Fullname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Office { get; set; }

        public DateTime Date { get; set; }
        public string Note { get; set; }
        public bool Present { get;set; }
        public bool SentViaEmail { get;set; }
        public bool IVResponse { get;set; }
        public string Token { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageKey { get; set; }
    }
}
