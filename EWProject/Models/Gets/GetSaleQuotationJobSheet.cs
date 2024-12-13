namespace Client.Models.Gets
{
    public class GetSaleQuotationJobSheet
    {
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; } = null!;
        public string SaleQuotationDate { get; set; } = null!;
        public string Tel { get; set; } = null!;
        public string CustomerRef { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Route { get; set; } = null!;
        public double Total { get; set; }
        public string DocNum { get; set; } = null!;
        public int DocEntry { get; set; }
        public string Currency { get; set; }
        public List<GetItemSaleQuotationJobSheet> Lines { get; set; } = null!;
    }
}
