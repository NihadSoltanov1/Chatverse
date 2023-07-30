using Chatverse.Application.Common.Interfaces;
using Chatverse.Application.Common.Results;
using Chatverse.Application.Exceptions;
using Chatverse.Application.Features.Command.Post.DeletePost;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Command.Comment.DeleteComment
{
   
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommandRequest, IDataResult<DeleteCommentCommandRequest>>
    {
        private readonly IApplicationDbContext _context;

        public DeleteCommentCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IDataResult<DeleteCommentCommandRequest>> Handle(DeleteCommentCommandRequest request, CancellationToken cancellationToken)
        {

            Domain.Entities.Comment deleteComment = _context.Comments.FirstOrDefault(c => c.Id == request.Id);
            if (deleteComment is null) throw new NotFoundException("Comment not found");
            _context.Comments.Remove(deleteComment);
            await _context.SaveChangesAsync(cancellationToken);
            return new SuccessDataResult<DeleteCommentCommandRequest>(request, "Comment deleted successfully");
        }
    }
}
