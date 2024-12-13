namespace Client.Models.Gets
{
    public class GetItemSaleQuotation
    {
        public string ItemCode { get; set; } = null!;
        public string ItemName { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
