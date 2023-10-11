namespace EvetWebsite.Data.Models
{
    public class Contact
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Office { get; set; }
        public string Message { get; set; }

        public DateTime Date { get; set; } 
    }
}
