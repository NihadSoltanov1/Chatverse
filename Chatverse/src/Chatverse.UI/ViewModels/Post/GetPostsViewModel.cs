namespace Chatverse.UI.ViewModels.Post
{
    public class GetPostsViewModel
    {
        public string FullName { get; set; }
        public string? Content { get; set; }
        public List<string>? Media { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
