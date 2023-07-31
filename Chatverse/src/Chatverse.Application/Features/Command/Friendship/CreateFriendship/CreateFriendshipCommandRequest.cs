using Chatverse.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Command.Friendship.CreateFriendship
{
    public record CreateFriendshipCommandRequest : IRequest<IDataResult<CreateFriendshipCommandRequest>>
    {
        public string ReceiverId { get; set; }
    }
}
