using Chatverse.Application.Common.Interfaces;
using Chatverse.Application.Common.Results;
using Chatverse.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Chatverse.Application.Features.Command.AppUser.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommandRequest, IResult>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly UserManager<Domain.Identity.AppUser> _userManager;

        public ChangePasswordCommandHandler(ICurrentUserService currentUserService, UserManager<Domain.Identity.AppUser> userManager)
        {
            _currentUserService = currentUserService;
            _userManager = userManager;
        }

        public async Task<IResult> Handle(ChangePasswordCommandRequest request, CancellationToken cancellationToken)
        {
            var currentUser = await _userManager.FindByNameAsync(_currentUserService.UserName);
            var isPasswordValid = await _userManager.CheckPasswordAsync(currentUser, request.CurrentPassword);
            if (isPasswordValid)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(currentUser);
                var result = await _userManager.ResetPasswordAsync(currentUser, token, request.NewPassword);
                if(result.Succeeded)
                {
                    return new Result(true, "Password changed successfully");
                }
                else
                {
                    throw new ChangePasswordException("Password couldn't change. Check your information");
                }
            }
            else
            {
                throw new ChangePasswordException("Current password is wrong");
            }

        }
    }
}
