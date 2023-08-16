using Chatverse.UI.ViewModels.Comments;

namespace Chatverse.UI.ViewModels.Post
{
    public class GetPostsViewModel
    {
        public int? PostId { get; set; }
        public string? FullName { get; set; }
        public  string? CurrentUser { get; set; }
        public string? Content { get; set; }
        public List<string>? Media { get; set; }
        public List<GetCommentsByPostIdViewModel>? Comments { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
