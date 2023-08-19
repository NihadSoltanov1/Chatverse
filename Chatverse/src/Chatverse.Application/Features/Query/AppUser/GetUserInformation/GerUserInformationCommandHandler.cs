using Chatverse.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Chatverse.Application.Features.Query.AppUser.GetUserInformation
{
    public class GerUserInformationCommandHandler : IRequestHandler<GetUserInformationCommandRequest, GetUserInformationCommandResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly UserManager<Domain.Identity.AppUser> _userManager;

        public GerUserInformationCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, UserManager<Domain.Identity.AppUser> userManager)
        {
            _context = context;
            _currentUserService = currentUserService;
            _userManager = userManager;
        }

        public async Task<GetUserInformationCommandResponse> Handle(GetUserInformationCommandRequest request, CancellationToken cancellationToken)
        {
            var currentUser = await _userManager.FindByNameAsync(_currentUserService.UserName);

            GetUserInformationCommandResponse response = new GetUserInformationCommandResponse()
            {
                Username = currentUser.UserName,
                Fullname = currentUser.FullName,
                ProfilePicture = currentUser.ProfilePicture,
                Email = currentUser.Email,
                Privicy = currentUser.Privicy,
                About = currentUser.About
            };
            return response;
        }
    }
}
