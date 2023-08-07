using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Query.Comment.GetCommentByPostId
{
    public class GetCommentByPostIdQueryRequest : IRequest<GetCommentByPostIdQueryResponse>
    {
        public int PostId { get; set; }
    }
}
