namespace Client.Models.Request
{
    public class PostPurchaseRequestRequest
    {
        public string ServiceOrItemType { get; set; } = null!;
        public string RequestBy { get; set; } = null!;
        public string RequireDate { get; set; } = null!;
        public string Remarks { get; set; } = null!;
        public List<PurchaseRequestLines> Lines { get; set; } = null!;
    }
    public class PurchaseRequestLines
    {
        public string ItemCode { get; set; } = null!;
        public double Price { get; set; }
        public string Remarks { get; set; } = null!;
    }
}
