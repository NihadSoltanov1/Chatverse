namespace Chatverse.Application.Features.Query.Post.GetPostByAuthorUserId;

public class GetPostByAuthorUserIdQueryHandler : IRequestHandler<GetPostByAuthorUserIdQueryRequest, GetPostByAuthorUserIdQueryResponse>
{
    readonly ICurrentUserService _currentUser;
    readonly UserManager<Domain.Identity.AppUser> _userManager;
    readonly IApplicationDbContext _context;
    readonly IMediator _mediator;
    public GetPostByAuthorUserIdQueryHandler(ICurrentUserService currentUser, UserManager<Domain.Identity.AppUser> userManager, IApplicationDbContext context, IMediator mediator)
    {
        _currentUser = currentUser;
        _userManager = userManager;
        _context = context;
        _mediator = mediator;
    }

    public async Task<GetPostByAuthorUserIdQueryResponse> Handle(GetPostByAuthorUserIdQueryRequest request, CancellationToken cancellationToken)
    {
        var currentUser = await _userManager.FindByNameAsync(_currentUser.UserName);
        if (currentUser is null) throw new NotFoundException();
        var posts = await _context.Posts.Where(p => p.AppUserId == currentUser.Id).ToListAsync();
        if (!posts.Any()) throw new NotFoundAnyPostException("You haven't shared any posts yet");
        List<GetMyPosts> getMyPosts = new List<GetMyPosts>();
        
        posts.ForEach(async post =>
        {
            if (post.State == true)
            {
            var comment = await _mediator.Send(new GetCommentByPostIdQueryRequest() { PostId = post.Id });
                bool isLike = false;
                var like = _context.Likes.FirstOrDefault(l => l.PostId == post.Id && l.AppUserId == currentUser.Id);
                isLike = like == null ? false : true;
                var getPosts = new GetMyPosts()
                {
                    PostId = post.Id,
                    Content = post.Content,
                    FullName = currentUser.FullName,
                    Media = _context.PostImages.Where(p => p.PostId == post.Id)
                        .Select(i => i.FilePath).ToList(),
                    CreateDate = post.CreatedDate,
                    Comments = comment.Comments,
                    IsLike = isLike

            };
            getMyPosts.Add(getPosts);
            }
        });
        if(getMyPosts is null) { throw new NotFoundException("There aren't any posts..."); }
        return new GetPostByAuthorUserIdQueryResponse()
        {
            Posts = getMyPosts
        };
    }
}
