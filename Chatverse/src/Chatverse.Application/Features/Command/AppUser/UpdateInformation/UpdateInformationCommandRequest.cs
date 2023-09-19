namespace Chatverse.Application.Features.Command.AppUser.UpdateInformation;

public class UpdateInformationCommandRequest : IRequest<Common.Results.IResult>
{
    public string? Username { get; set; }
    public string? Fullname { get; set; }
    public string? Email { get; set; }
    public string? ProfilePicture { get; set; }
    public bool Privicy { get; set; }
    public string? About { get; set; }
}
