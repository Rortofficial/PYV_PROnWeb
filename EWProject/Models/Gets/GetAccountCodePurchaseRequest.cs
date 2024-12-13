namespace Client.Models.Gets
{
    public class GetAccountCodePurchaseRequest
    {
        public string AccountCode { get; set; } = null!;
        public string AccountName { get; set; } = null!;
        public double AccountBalance { get; set; }
    }
}
