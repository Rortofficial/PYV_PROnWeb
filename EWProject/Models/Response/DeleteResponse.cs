namespace Client.Models.Response
{
    public class DeleteResponse
    {
        public int ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
        public string DocNum { get; set; }
        public string EDocNum { get; set; }
        public string DocEntry { get; set; }
    }
}
