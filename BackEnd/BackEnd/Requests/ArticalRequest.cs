namespace BackEnd.Requests
{
    public class ArticalRequest {
        public string Title { get; set; }
        public string? Slug { get; set; }
        public string Content { get; set; }
        public string? Summary { get; set; }
        public string? ImageUrl { get; set; }

        // Author
        public int AuthorId { get; set; }

        public bool IsPushiled { get; set; }

        public int? CatertoCategoryId { get; set; }
    }
}
