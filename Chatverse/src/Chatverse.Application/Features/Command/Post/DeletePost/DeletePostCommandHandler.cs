using Chatverse.Application.Common.Interfaces;
using Chatverse.Application.Common.Results;
using Chatverse.Application.Exceptions;
using Chatverse.Application.Features.Command.Post.CreatePost;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Command.Post.DeletePost
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommandRequest, IDataResult<DeletePostCommandRequest>>
    {
        private readonly IApplicationDbContext _context;

        public DeletePostCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IDataResult<DeletePostCommandRequest>> Handle(DeletePostCommandRequest request, CancellationToken cancellationToken)
        {

            Domain.Entities.Post deletePost = _context.Posts.FirstOrDefault(p => p.Id == request.Id);
            if (deletePost is null) throw new NotFoundException("Post not found");
            _context.Posts.Remove(deletePost);
            await _context.SaveChangesAsync(cancellationToken);
            return new SuccessDataResult<DeletePostCommandRequest>(request, "Post deleted successfully");
        }
    }
}
