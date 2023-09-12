using System.ComponentModel.DataAnnotations;

namespace Chatverse.UI.ViewModels.Auth
{
    public class LoginViewModel
    {
        public string? UsernameOrEmail { get; set; }
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
