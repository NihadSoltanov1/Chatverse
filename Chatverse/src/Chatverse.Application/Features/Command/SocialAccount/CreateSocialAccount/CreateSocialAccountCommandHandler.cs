using Chatverse.Application.Common.Interfaces;
using Chatverse.Application.Common.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Chatverse.Application.Features.Command.SocialAccount.CreateSocialAccount
{
    public class CreateSocialAccountCommandHandler : IRequestHandler<CreateSocialAccountCommandRequest, IResult>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly UserManager<Domain.Identity.AppUser> _userManager;
        private readonly IApplicationDbContext _context;

        public CreateSocialAccountCommandHandler(ICurrentUserService currentUserService, UserManager<Domain.Identity.AppUser> userManager, IApplicationDbContext context)
        {
            _currentUserService = currentUserService;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IResult> Handle(CreateSocialAccountCommandRequest request, CancellationToken cancellationToken)
        {
            var currentUser = await _userManager.FindByNameAsync(_currentUserService.UserName);
            if (request.SocialMedias is not null)
            {
               foreach(var social in request.SocialMedias)
                {

                    Domain.Entities.SocialAccount newSocialAccount = new Domain.Entities.SocialAccount()
                    {
                        
                        UserId = currentUser.Id,
                        Link = social.Url,

                    };
                    if(newSocialAccount.Link is not null)
                    {
                        newSocialAccount.Category = social.Category;
                    }
                    if (newSocialAccount.Link != null && newSocialAccount.Category != null)
                    {
                        _context.SocialAccounts.Add(newSocialAccount);
                        await _context.SaveChangesAsync(cancellationToken);
                    }
                }
               
            }
            return new Result(true, "Complate");
        }
    }
}
