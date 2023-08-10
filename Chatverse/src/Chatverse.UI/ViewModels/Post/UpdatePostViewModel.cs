namespace Chatverse.UI.ViewModels.Post
{
    public class UpdatePostViewModel
    {
        public int UpdatePostId { get; set; }
        public string? UpdateContent { get; set; }
        public IFormFileCollection? UpdateMedia{ get; set; } 
    }
}
