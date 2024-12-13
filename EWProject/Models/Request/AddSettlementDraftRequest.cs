namespace Client.Models.Request
{
    public class AddSettlementDraftRequest
    {
        public string RefNo { get; set; }
        public int DocEntry { get; set; }
        public double TotalPaid { get; set; }
        public double GrandTotal { get; set; }
        public string UserID { get; set; }
        public List<AddSettlementLine> Lines { get; set; }
    }
    public class AddSettlementLine
    {
        public int LineId { get; set; }
        public string ItemCode { get; set; }
        public double Paid { get; set; }
        public string JobNumber { get; set; }
        public string CustomerCode { get; set; }
        public string InvNumber { get; set; }
        public string DeclarationNo { get; set; }
        public string RemarkOrRisk { get; set; }
    }
}
