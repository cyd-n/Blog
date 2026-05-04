namespace BackEnd.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }

        public string HashedPass { get; set; }

        public string ProfielImgUrl { get; set; }

        public DateTime CreatedTime { get; set; }
    }
}
