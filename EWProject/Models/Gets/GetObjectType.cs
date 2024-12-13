namespace Client.Models.Gets
{
    public class GetObjectType
    {
        public string ObjectType { get; set; }
        public string ObjectAttribute { get; set; }
        public string SubAttribute { get; set; }
        public bool Query { get; set; }
        public string Script { get; set; }
    }
}
