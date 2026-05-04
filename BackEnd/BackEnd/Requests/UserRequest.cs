namespace BackEnd.Requests
{
    public class UserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }

        public string HashedPass { get; set; }

        public string? ProfielImgUrl { get; set; }
    }
}
