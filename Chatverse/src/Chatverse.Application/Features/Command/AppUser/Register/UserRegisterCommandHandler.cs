using Chatverse.Application.Common.Interfaces;
using Chatverse.Application.Common.Results;
using Chatverse.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;
        private readonly IEmailService _email;
        public UserRegisterCommandHandler(UserManager<Domain.Identity.AppUser> userManager, IConfiguration configuration, IEmailService email)
        {
            _userManager = userManager;
            _configuration = configuration;
            _email = email;
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
            string confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
            var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);
            string url = $"{_configuration["ApiStatic:MvcUrl"]}/auth/confirmemail?userid={newUser.Id}&token={validEmailToken}";
            _email.SendMail(newUser.Email, "Confirm Your Email", $"<h1>Welcome ChatVerse</h1>" + $"<p>Please confirm your email  by <a href='{url}'>Clicking here</a> </p>");
            return new Result(true, "User created succesfully");
        }
    }
}
