using Chatverse.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Command.Friendship.AcceptFriendRequest
{
    public class AcceptFriendRequestCommandRequest : IRequest<IResult>
    {
        public int FrienshipId { get; set; }
    }
}
