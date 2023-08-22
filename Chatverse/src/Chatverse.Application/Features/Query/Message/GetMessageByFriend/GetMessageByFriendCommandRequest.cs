using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Query.Message.GetMessageByFriend
{
    public class GetMessageByFriendCommandRequest : IRequest<List<GetMessageByFriendCommandResponse>>
    {
        public string FriendId { get; set; }
        public string? CurrentUserId { get; set; }
    }


}
