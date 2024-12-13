namespace Client.Models.Request
{
    public class PostMessage
    {
        public string UserID { get; set; }
        public string MessageHeader { get; set; }
        public string MessageDescription { get; set; }
    }
}
