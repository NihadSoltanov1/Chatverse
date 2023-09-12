using Amazon.Runtime.Internal;
using Chatverse.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Command.AppUser.ForgetPassword
{
    public class ForgetPasswordCommandRequest : IRequest<IResult>
    {
        public string Email { get; set; }
    }
}
