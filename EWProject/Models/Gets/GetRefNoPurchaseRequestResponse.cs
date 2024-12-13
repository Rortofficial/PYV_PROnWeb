namespace Client.Models.Gets
{
    public class GetRefNoPurchaseRequestResponse
    {
        public string ErrorMessage { get; set; } = null!;
        public int ErrorCode { get; set; }
        public string Data { get; set; } = null!;
        public string DocNum { get; set; }
    }
}
