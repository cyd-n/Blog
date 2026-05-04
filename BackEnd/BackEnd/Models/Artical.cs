namespace BackEnd.Models
{
    public class Artical
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public string? Summary { get; set; }
        public string? ImageUrl { get; set; }

        // Author
        public int AuthorId { get; set; }

        // Status
        public DateTime CreatedTime { get; set; }
        public DateTime? LastUpdatedTime { get; set; }
        public DateTime? LastPolishedTime { get; set; }

        public DateTime? DeletedTime { get; set; }

        public bool IsPushiled { get; set; }

        public int? CatertoCategoryId { get; set; }

    }
}