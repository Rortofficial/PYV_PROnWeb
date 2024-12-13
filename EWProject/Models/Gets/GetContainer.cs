namespace Client.Models.Gets
{
    public class GetContainer
    {
        public string ItemCode { get; set; } = null!;
        public string ItemName { get; set; } = null!;
        public string ItemType { get; set; } = null!;
        public double ItemPrice { get; set; }
        public string VendorCode { get; set; } = null!;
        public string VendorName { get; set; } = null!;
        public string Owner { get; set; } = null!;
        public string Yard { get; set; } = null!;
        public string LOLOorYard { get; set; } = null!;
        public string FCLorLCL { get; set; } = null!;
    }
}
