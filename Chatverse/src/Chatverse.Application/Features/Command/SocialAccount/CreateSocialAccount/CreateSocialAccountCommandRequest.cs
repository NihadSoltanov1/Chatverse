namespace Chatverse.Application.Features.Command.SocialAccount.CreateSocialAccount;

public class CreateSocialAccountCommandRequest : IRequest<Common.Results.IResult>
{
    public List<SocialMedia> SocialMedias { get; set; }
}
public class SocialMedia
{
    public string Category { get; set; }
    public string Url { get; set; }
}
