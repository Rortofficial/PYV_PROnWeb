namespace Client.Models.Gets
{
    public class GetItemPriceList
    {
        public string ItemCode { get; set; } = null!;
        public double Price { get; set; }
        public string IsEnable { get; set; } = null!;
    }
}
