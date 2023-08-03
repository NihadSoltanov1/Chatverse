using Chatverse.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Command.Post.CreatePost
{
    public record CreatePostCommandRequest : IRequest<IDataResult<CreatePostCommandRequest>>
    {
        public string? Content { get; set; }
        public List<string>? MediaLocation { get; set; }
    }
}
