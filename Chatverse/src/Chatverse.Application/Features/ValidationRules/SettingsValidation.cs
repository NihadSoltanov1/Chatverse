namespace Chatverse.Application.Features.ValidationRules;

public class SettingsValidation : AbstractValidator<CreateSocialAccountCommandRequest>
{
    public SettingsValidation()
    {
        RuleForEach(request => request.SocialMedias).NotEmpty().WithMessage("Can not be empty.")
       .ChildRules(socialMedia =>
       {
           socialMedia.RuleFor(media => media.Url)
               .Must(BeValidUrl).WithMessage(x=>x.Category + ": Not valid URL format.");
       });
    }
    private bool BeValidUrl(string url)
    {
        Uri uriResult;
        return Uri.TryCreate(url, UriKind.Absolute, out uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}
