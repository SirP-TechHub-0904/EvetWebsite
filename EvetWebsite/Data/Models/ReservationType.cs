namespace EvetWebsite.Data.Models
{
    public class ReservationType
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageKey { get; set; }
    }
}
