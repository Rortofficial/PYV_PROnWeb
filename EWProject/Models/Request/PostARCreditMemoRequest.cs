namespace Client.Models.Request
{
    public class PostARCreditMemoRequest
    {
        public string CustomerCode { get; set; }
        public string BpCurrency { get; set; }
        public string BaseEntry { get; set; }
        public string JobNumber { get; set; }
        public string CreateUser { get; set; }
        public string CostCenter { get; set; }
        public List<PostARCreditMemoLine> Lines { get; set; }
    }
    public class PostARCreditMemoLine
    {
        public string ItemCode { get; set; }
        public double Paid { get; set; }
        public string Vat { get; set; }
        public int LineNum { get; set; }
        public string Remark { get; set; }
    }
}
