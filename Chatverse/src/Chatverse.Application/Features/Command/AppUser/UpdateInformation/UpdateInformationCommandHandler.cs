namespace Chatverse.Application.Features.Command.AppUser.UpdateInformation;

public class UpdateInformationCommandHandler : IRequestHandler<UpdateInformationCommandRequest, Common.Results.IResult>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly UserManager<Domain.Identity.AppUser> _userManager;

    public UpdateInformationCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, UserManager<Domain.Identity.AppUser> userManager)
    {
        _context = context;
        _currentUserService = currentUserService;
        _userManager = userManager;
    }

    public async Task<Common.Results.IResult> Handle(UpdateInformationCommandRequest request, CancellationToken cancellationToken)
    {
        var currentUser = await _userManager.FindByNameAsync(_currentUserService.UserName);
        currentUser.UserName = request.Username;
        currentUser.FullName = request.Fullname;
        currentUser.ProfilePicture = request.ProfilePicture;
        currentUser.Privicy = request.Privicy;
        request.Email = request.Email;
        currentUser.About = request.About;

        var result = await _userManager.UpdateAsync(currentUser);
        if (result.Succeeded)
        {
            return new Result(true, "Successfully");
        }
        return new Result(false, "Failed");
    }
}
