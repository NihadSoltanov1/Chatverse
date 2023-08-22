namespace Chatverse.UI.ViewModels.Message
{
    public class GetAllMessageViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string SenderId { get; set; }
        public string SenderUsername { get; set; }
        public string SenderProfilePicture { get; set; }
        public string ReceiverId { get; set; }
        public string ReceiverUsername { get; set; }
        public string ReceiverProfilePicture { get; set; }
        public DateTime SendDate { get; set; }
    }
}
