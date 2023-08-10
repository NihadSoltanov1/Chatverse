using Chatverse.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Command.Post.UpdatePost
{
    public class UpdatePostCommandRequest : IRequest<IDataResult<List<UpdatePostCommandResponse>>>
    {
        public int UpdatePostId { get; set; }
        public string? UpdateContent { get; set; }
        public List<string>? UpdateMedia { get; set; }
    }
}
