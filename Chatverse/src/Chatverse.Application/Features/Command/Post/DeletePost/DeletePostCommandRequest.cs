using Chatverse.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Command.Post.DeletePost
{
    public class DeletePostCommandRequest : IRequest<IDataResult<DeletePostCommandRequest>>
    {
        public int Id { get; set; }
    }
}
