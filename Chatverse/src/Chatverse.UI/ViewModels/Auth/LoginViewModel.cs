namespace Chatverse.UI.ViewModels.Auth
{
    public class LoginViewModel
    {
        public string? UsernameOrEmail { get; set; }
        public string? Password { get; set; }
        public bool IsRemember { get; set; } = false;
    }
}
