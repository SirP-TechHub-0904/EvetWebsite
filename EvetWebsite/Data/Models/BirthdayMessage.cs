namespace EvetWebsite.Data.Models
{
    public class BirthdayMessage
    {
        public long Id { get; set; }
        public string Fullname { get; set; } 
        public string Email { get; set; }
        public string Office { get; set; }
        
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public bool Disable { get;set;}
    }
}
