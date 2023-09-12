using Amazon.Runtime.Internal;
using Chatverse.Application.Common.Results;
using Chatverse.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Command.AppUser.ForgetPassword.UpdatePassword
{
    internal class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommandRequest, IResult>
    {
        private readonly UserManager<Domain.Identity.AppUser> _userManager;

        public UpdatePasswordCommandHandler(UserManager<Domain.Identity.AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IResult> Handle(UpdatePasswordCommandRequest request, CancellationToken cancellationToken)
        {
           var user = await _userManager.FindByIdAsync(request.UserId);
            if (user is null) throw new NotFoundException("User not found.");
            
            var decodedToken = WebEncoders.Base64UrlDecode(request.Token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);
            var result = await _userManager.ResetPasswordAsync(user,normalToken,request.Password);
            if (result.Succeeded) return new Result(true, "Email confirm succesfully");
            throw new ChangePasswordException("An error occurred while attempting to change the password.");
        }
    }
}
