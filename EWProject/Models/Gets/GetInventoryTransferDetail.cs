namespace Client.Models.Gets
{
    public class GetInventoryTransferDetail
    {
        public string IssueDate { get; set; }
        public string DocNum { get; set; }
        public string IssueBy { get; set; }
        public string Comment { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public List<GetInventoryTransferDetailLine> Lines { get; set; }
    }
    public class GetInventoryTransferDetailLine
    {
        public string Project { get; set; }
        public string ContainerCode { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
    }
}
