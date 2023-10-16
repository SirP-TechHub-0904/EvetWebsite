namespace EvetWebsite.Data.Models
{
    public class Committee
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Office { get; set; }
        public long CommitteeCategoryId { get; set; }
        public CommitteeCategory CommitteeCategory { get; set; }
    }
}
