namespace Chatverse.UI.DTOs.Post
{
    public class UpdatePostDto
    {
        public int UpdatePostId { get; set; }
        public string? UpdateContent { get; set; }
        public List<string>? UpdateMedia { get; set; }
    }
}
