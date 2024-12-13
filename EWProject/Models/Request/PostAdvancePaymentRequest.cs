namespace Client.Models.Request
{
    public class PostAdvancePaymentRequest
    {
        public int Series { get; set; }
        public string SeriesName { get; set; }
        public DateTime DocDate { get; set; }
        public string Remark { get; set; }
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public string Ref3 { get; set; }
        public string ProjectManagement { get; set; }
        public string DocNum { get; set; }
        public string DocEntry { get; set; }
        public string UserID { get; set; }
        public string WebID { get; set; }
        public List<AdvancePaymentRequestLine> Lines { get; set; }
    }
    public class AdvancePaymentRequestLine
    {
        public int id { get; set; }
        public string BpCode { get; set; }
        public string bpCodeOrBpName { get; set; }
        public string AccountCode { get; set; }
        public string accountCodeOrAccountName { get; set; }
        public double amount { get; set; }
    }
}
