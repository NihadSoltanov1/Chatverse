namespace Chatverse.Application.Features.Command.Post.CreatePost;

  public class CreatePostCommandHandler : IRequestHandler<CreatePostCommandRequest, IDataResult<CreatePostCommandRequest>>
{
   private readonly ICurrentUserService _currentUserService;
   private readonly UserManager<Domain.Identity.AppUser> _userManager;
   private readonly IApplicationDbContext _context;
    private readonly IGoogleCloudService _googleCloudService;
    public CreatePostCommandHandler(ICurrentUserService currentUserService, UserManager<Domain.Identity.AppUser> userManager, IApplicationDbContext context, IGoogleCloudService googleCloudService)
    {
        _currentUserService = currentUserService;
        _userManager = userManager;
        _context = context;
        _googleCloudService = googleCloudService;
    }

    public async Task<IDataResult<CreatePostCommandRequest>> Handle(CreatePostCommandRequest request, CancellationToken cancellationToken)
    {
        var currentUser = await _userManager.FindByNameAsync(_currentUserService.UserName);
        if (currentUser is null) throw new UnauthorizedLoginException("Login to your account to share post");
        Domain.Entities.Post post = new()
        {
            AppUserId = currentUser.Id,
            Content = request.Content,
            State=true
        };
        await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync(cancellationToken);
        string rootFolder = @"wwwroot\";
        foreach (string path in request.MediaLocation)
        {
            _googleCloudService.UploadFileToCloud(path);
            string returnPath = path.Substring(path.IndexOf(rootFolder, StringComparison.OrdinalIgnoreCase) + rootFolder.Length).Replace("\\", "/");
            PostImage postImage = new PostImage
            {
                PostId = post.Id,
                FilePath = returnPath,
                State=true
                
            };
            await _context.PostImages.AddAsync(postImage);
            await _context.SaveChangesAsync(cancellationToken);
        }
             
        return new SuccessDataResult<CreatePostCommandRequest>(request, "Post added successfully");
    }
}
