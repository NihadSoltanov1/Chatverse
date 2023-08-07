using Chatverse.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Query.Comment.GetCommentByPostId
{
    public class GetCommentByPostIdHandler : IRequestHandler<GetCommentByPostIdQueryRequest, GetCommentByPostIdQueryResponse>
    {
        readonly IApplicationDbContext _context;

        public GetCommentByPostIdHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetCommentByPostIdQueryResponse> Handle(GetCommentByPostIdQueryRequest request, CancellationToken cancellationToken)
        {
            List<GetCommentByPostId> commentList = _context.Comments
                 .Where(comment => comment.PostId == request.PostId)
                 .Select(comment => new GetCommentByPostId
                 {
                  Content = comment.Content,
                  FullName = comment.AppUser.FullName,
                  Id = comment.Id

                 }).ToList();  
            return new GetCommentByPostIdQueryResponse()
            {
                Comments = commentList
            };
        }
    }
}
