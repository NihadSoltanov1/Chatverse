using Chatverse.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Command.Post.UpdatePost
{
    public class UpdatePostCommandRequest : IRequest<IDataResult<UpdatePostCommandRequest>>
    {
        public int Id { get; set; }
        public string? Content { get; set; } 
        public string? Media { get; set; } 
    }
}
