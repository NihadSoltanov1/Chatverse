namespace Chatverse.UI.DTOs.Message
{
    public class GetAllMessageDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string SenderId { get; set; }
        public string SenderUsername { get; set; }
        public string SenderProfilePicture { get; set; }
        public string ReceiverId { get; set; }
        public string ReceiverUsername { get; set; }
        public string ReceiverProfilePicture { get; set; }
        public string SendDate { get; set; }
    }
}
