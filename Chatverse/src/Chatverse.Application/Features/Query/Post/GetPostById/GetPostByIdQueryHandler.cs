using Chatverse.Application.Common.Interfaces;
using Chatverse.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatverse.Application.Features.Query.Post.GetPostById
{
    public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQueryRequest, GetPostByIdQueryResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly UserManager<Domain.Identity.AppUser> _userManager;
        public GetPostByIdQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService, UserManager<Domain.Identity.AppUser> userManager)
        {
            _context = context;
            _currentUserService = currentUserService;
            _userManager = userManager;
        }

        public async Task<GetPostByIdQueryResponse> Handle(GetPostByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var currentUser = await _userManager.FindByNameAsync(_currentUserService.UserName);
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == request.Id);
            if (post == null) throw new NotFoundException("Post couldn't found");
            return new GetPostByIdQueryResponse()
            {
                Content = post.Content,
                PostId = post.Id,
                FullName = currentUser.FullName,
                Media = _context.PostImages.Where(i => i.PostId == post.Id).Select(i=>i.FilePath).ToList(),
                CreateDate = post.CreatedDate
            };
        }
    }
}
