namespace Client.Models.Gets
{
    public class GetMessage
    {
        public string UserName { get; set; }
        public string MessageHeader { get; set; }
        public string MessageDescription { get; set; }
        public int MessageCount { get; set; }
        public int AlertID { get; set; }
    }
}
