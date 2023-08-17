using Chatverse.Application.Features.Query.Friendship.GetAllRequest;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Query.Friendship.GetAllFriends
{
    public class GetAllFriendsQueryRequest : IRequest<List<GetAllFriendsQueryResponse>>
    {
    }
}
