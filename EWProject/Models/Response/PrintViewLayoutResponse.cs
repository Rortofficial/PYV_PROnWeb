namespace Client.Models.Response
{
    public class PrintViewLayoutResponse
    {
        public string ErrorMessage { get; set; } = null!;
        public int ErrorCode { get; set; }
        public byte[] Data { get; set; } = null!;
        public string ApplicationType { get; set; } = null!;
        public string FileName { get; set; } = null!;
    }
}
