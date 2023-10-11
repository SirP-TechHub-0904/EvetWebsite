namespace EvetWebsite.Data.Models
{
    public class Reservation
    {
        public long Id { get; set; }
        public string Fullname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Office { get; set; }
        public string Room { get; set; }
        public string RoomType { get; set; }
        public long ReservationTypeId { get; set; }
        public ReservationType ReservationType { get; set; }
        public DateTime Date { get;set; }
        public string Note { get; set;}

        
    }
}
