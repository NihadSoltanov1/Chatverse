namespace Chatverse.UI.DTOs.Auth
{
    public class RegisterDto
    {
         public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string? ProfilePicture { get; set; }
        public bool? IsAgree { get; set; } = false;
    }
}
