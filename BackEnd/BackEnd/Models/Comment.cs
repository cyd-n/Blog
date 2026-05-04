namespace BackEnd.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int ArticalId { get; set; }
        public int UserId { get; set; }

        public string Content { get; set; }

        public DateTime CreatedTime { get; set; }
    }
}
