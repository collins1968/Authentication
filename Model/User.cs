namespace Auth.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string email { get; set; } = string.Empty;

        public string role { get; set; } = "user";
    }
}
