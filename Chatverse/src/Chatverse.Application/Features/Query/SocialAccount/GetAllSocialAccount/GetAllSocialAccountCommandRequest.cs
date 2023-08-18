using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Query.SocialAccount.GetAllSocialAccount
{
    public class GetAllSocialAccountCommandRequest : IRequest<List<GetAllSocialAccountCommandResponse>>
    {
    }
}
