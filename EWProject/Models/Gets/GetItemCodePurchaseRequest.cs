namespace Client.Models.Gets
{
    public class GetItemCodePurchaseRequest
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public double Price { get; set; }
        public string ServiceType { get; set; }
    }
}
