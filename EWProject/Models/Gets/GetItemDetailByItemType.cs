namespace Client.Models.Gets
{
    public class GetItemDetailByItemType
    {
        public int LineNum { get; set; }
        public string ItemCode { get; set; } = null!;
        public string ItemName { get; set; } = null!;
        public string uomCodeList { get; set; }
        public double PriceSelling { get; set; }
        public double PriceCosting { get; set; }
        public double qtyCosting { get; set; }
        public double qtySelling { get; set; }
        public string ItemType { get; set; }
        public string Remarks { get; set; }
    }
}
