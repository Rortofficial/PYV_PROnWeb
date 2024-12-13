namespace Client.Models.Gets
{
    public class GetARInvoiceWithJobNumberByDocEntry
    {
        public string DocEntry { get; set; }
        public string DocNum { get; set; }
        public string CustomerRef { get; set; }
        public string JobNumber { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string IssueDate { get; set; }
        public double DocTotal { get; set; }
        public string Remark { get; set; }
        public string Status { get; set; }
        public string ContactPerson { get; set; }
        public string Currency { get; set; }
        public List<GetARInvoiceWithJobNumberByDocEntryLine> Lines { get; set; }
    }
    public class GetARInvoiceWithJobNumberByDocEntryLine
    {
        public int RowNum { get; set; }
        public int LineNumber { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ServiceType { get; set; }
        public double LineTotal { get; set; }
        public string Remarks { get; set; }
    }
}
