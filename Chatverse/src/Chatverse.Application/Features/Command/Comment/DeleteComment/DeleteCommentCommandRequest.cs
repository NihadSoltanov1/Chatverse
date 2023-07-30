using Chatverse.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Command.Comment.DeleteComment
{
    public class DeleteCommentCommandRequest : IRequest<IDataResult<DeleteCommentCommandRequest>>
    {
        public int Id { get; set; }

    }
}
