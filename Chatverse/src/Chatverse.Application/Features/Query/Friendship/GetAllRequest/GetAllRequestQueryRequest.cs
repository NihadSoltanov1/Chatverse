using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Query.Friendship.GetAllRequest
{
    public class GetAllRequestQueryRequest : IRequest<List<GetAllRequestQueryResponse>>
    {
    }
}
