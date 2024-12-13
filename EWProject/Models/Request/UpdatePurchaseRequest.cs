namespace Client.Models.Request
{
    public class UpdatePurchaseRequest
    {
        public int DocEntryAD { get; set; }
        public int DocEntryPO { get; set; }
        public int DocEntryPR { get; set; }
        public int MaxLineNum { get; set; }
        public List<UpdatePurchaseRequestLine> Lines { get; set; }
    }
    public class UpdatePurchaseRequestLine
    {
        public int LineNumAD { get; set; }
        public int LineNumPO { get; set; }
        public int LineNumPR { get; set; }
        public string ItemCode { get; set; }
        public double Paid { get; set; }
        public double Amount { get; set; }
        public string JobNumber { get; set; }
        public string RemarkOrRisk { get; set; }
        public string CustomerCode { get; set; }
        public string DeclarationNo { get; set; }
        public string InvNumber { get; set; }
    }
}
