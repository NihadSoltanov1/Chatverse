namespace Chatverse.UI.ViewModels.Post
{
    public class GetPostByIdViewModel
    {
        public int PostId { get; set; }
        public string FullName { get; set; }
        public string? Content { get; set; }
        public List<string>? Media { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
