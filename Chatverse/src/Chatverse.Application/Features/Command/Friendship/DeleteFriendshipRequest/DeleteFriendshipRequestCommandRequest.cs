using Chatverse.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Command.Friendship.DeleteFriendshipRequest
{
    public class DeleteFriendshipRequestCommandRequest : IRequest<IResult>
    {
        public int FriendshipId { get; set; }
    }
}
