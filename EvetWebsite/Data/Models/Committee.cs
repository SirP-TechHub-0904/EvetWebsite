namespace EvetWebsite.Data.Models
{
    public class Committee
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool DisplayImage { get; set; }
        public string? Office { get; set; }
        public long CommitteeCategoryId { get; set; }
        public CommitteeCategory CommitteeCategory { get; set; }

        public string? ImageUrl { get; set; }
        public string? ImageKey { get; set; }
    }
}
