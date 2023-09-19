namespace Chatverse.Domain.Entities;
    public class PostImage 
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public bool State { get; set; }
    }

