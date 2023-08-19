namespace Chatverse.Application.Features.Query.AppUser.GetUserInformation
{
    public class GetUserInformationCommandResponse
    {
        public string? Username { get; set; }
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public string? ProfilePicture { get; set; }
        public bool Privicy { get; set; }
        public string? About { get; set; }
    }
}
