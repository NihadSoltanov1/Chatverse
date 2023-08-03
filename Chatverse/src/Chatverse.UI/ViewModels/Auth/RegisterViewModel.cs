using System.ComponentModel.DataAnnotations;

namespace Chatverse.UI.ViewModels.Auth
{
    public class RegisterViewModel
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password), Compare(nameof(Password))]
        public string PasswordConfirm { get; set; }
        public IFormFile? ProfilePicture { get; set; }
    }
}
