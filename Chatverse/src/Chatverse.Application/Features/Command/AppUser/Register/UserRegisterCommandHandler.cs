using Chatverse.Application.Common.Results;
using Chatverse.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Command.AppUser.Register
{
    public record UserRegisterCommandHandler : IRequestHandler<UserRegisterCommandRequest, IResult>
    {
        private readonly UserManager<Chatverse.Domain.Identity.AppUser> _userManager;

        public UserRegisterCommandHandler(UserManager<Domain.Identity.AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IResult> Handle(UserRegisterCommandRequest request, CancellationToken cancellationToken)
        {
            var anyUser = await _userManager.FindByEmailAsync(request.Email);

            if (anyUser is not null) throw new UserAlreadyExistException("This email already exist");
            anyUser = await _userManager.FindByNameAsync(request.Username);
            if (anyUser is not null) throw new UserAlreadyExistException("This username already exist");

            Domain.Identity.AppUser newUser = new()
            {
                FullName = request.FullName,
                UserName = request.Username,
                Email = request.Email,
                ProfilePicture = request.ProfilePicture
            };
            IdentityResult identityResult = await _userManager.CreateAsync(newUser, request.Password);
            if (!identityResult.Succeeded) throw new UserCreateFailedException("An unexpected error occurred while creating the user!");
            return new Result(true, "User created succesfully");
        }
    }
}
