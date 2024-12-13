namespace Client.Models.Gets
{
    public class GetListOfInventoryTransfer
    {
        public int DocEntry { get; set; }
        public string DocNum { get; set; }
        public string Status { get; set; }
        public int ContainerNumber { get; set; }
        public string DocDate { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string Remark { get; set; }
        public string IssueBy { get; set; }
    }
}
