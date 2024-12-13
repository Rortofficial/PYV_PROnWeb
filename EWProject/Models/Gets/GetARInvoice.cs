namespace Client.Models.Gets
{
    public class GetARInvoice
    {
        public int ID { get; set; }
        public int Row { get; set; }
        public string DocNum { get; set; }
        public string WebId { get; set; }
        public string JobNumber { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string IssueDate { get; set; }
        public double DocTotal { get; set; }
        public string Comment { get; set; }
    }
}
