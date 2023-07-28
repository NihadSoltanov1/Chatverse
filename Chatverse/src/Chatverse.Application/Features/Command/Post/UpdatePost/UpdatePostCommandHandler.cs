using Chatverse.Application.Common.Interfaces;
using Chatverse.Application.Common.Results;
using Chatverse.Application.Features.Command.Post.CreatePost;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Command.Post.UpdatePost
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommandRequest, IDataResult<UpdatePostCommandRequest>>
    {
        private readonly IApplicationDbContext _context;

        public UpdatePostCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IDataResult<UpdatePostCommandRequest>> Handle(UpdatePostCommandRequest request, CancellationToken cancellationToken)
        {

            Domain.Entities.Post updatePost = await _context.Posts.FirstOrDefaultAsync(p => p.Id == request.Id);
            switch (request)
            {
                case { Media: null, Content: null }: _context.Posts.Update(updatePost); break;
                default: 
                    updatePost.Content = request.Content;
                    updatePost.MediaLocation = request.Media;
                    break;
            }
            await _context.SaveChangesAsync(cancellationToken);
           return new SuccessDataResult<UpdatePostCommandRequest>(request, "Post update successfully");
        }
    }
}
