namespace Client.Models.Gets
{
    public class GetItemSaleQuotationJobSheet
    {
        public int lineNum { get; set; }
        public string itemCode { get; set; } = null!;
        public string itemName { get; set; } = null!;
        public double priceSelling { get; set; }
        public double priceCosting { get; set; }
        public double qtyCosting { get; set; }
        public double qtySelling { get; set; }
        public string itemType { get; set; }
        public string uomCodeList { get; set; }
        public string remarks { get; set; }
    }
}
