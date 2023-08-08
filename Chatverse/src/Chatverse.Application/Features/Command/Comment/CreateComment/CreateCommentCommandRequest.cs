using Chatverse.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Command.Comment.CreateComment
{
    public record CreateCommentCommandRequest : IRequest<IDataResult<CreateCommentCommandRequest>>
    {
        public int PostId { get; set; }
        public string Content { get; set; }
    }
}
