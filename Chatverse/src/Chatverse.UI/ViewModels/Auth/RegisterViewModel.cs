using System.ComponentModel.DataAnnotations;

namespace Chatverse.UI.ViewModels.Auth
{
    public class RegisterViewModel
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public IFormFile ProfilePicture { get; set; }
        public bool IsAgree { get; set; } 
    }
}
