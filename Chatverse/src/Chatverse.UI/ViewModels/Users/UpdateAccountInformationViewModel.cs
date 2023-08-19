namespace Chatverse.UI.ViewModels.Users
{
    public class UpdateAccountInformationViewModel
    {
        public string? Username { get; set; }
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public IFormFile ProfilePicture { get; set; }
        public bool Privicy { get; set; }
        public string? About { get; set; }
    }
}
