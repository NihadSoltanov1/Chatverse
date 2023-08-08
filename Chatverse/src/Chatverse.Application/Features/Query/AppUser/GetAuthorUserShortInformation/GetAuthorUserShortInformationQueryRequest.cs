using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Query.AppUser.GetAuthorUserShortInformation
{
    public record GetAuthorUserShortInformationQueryRequest : IRequest<GetAuthorUserShortInformationQueryResponse>
    {
        
    }
}
