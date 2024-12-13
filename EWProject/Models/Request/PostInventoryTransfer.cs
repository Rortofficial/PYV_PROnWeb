namespace Client.Models.Request
{
    public class PostInventoryTransfer
    {
        public string DocDate { get; set; }
        public string Comment { get; set; }
        public List<InventoryTransferLine> Lines { get; set; }
    }
    public class InventoryTransferLine
    {
        public string Type { get; set; }
        public string ItemCode { get; set; }
        public string ItemCodeExchange { get; set; }
        public string WarehouseFrom { get; set; }
        public string WarehouseTo { get; set; }
    }
}
