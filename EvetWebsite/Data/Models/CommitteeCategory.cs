namespace EvetWebsite.Data.Models
{
    public class CommitteeCategory
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public bool DisplayImage { get; set; }

        public ICollection<Committee> Committees { get; set; }
    }
}
