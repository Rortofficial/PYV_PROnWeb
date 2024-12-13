namespace Client.Models.Gets
{
    public class GetAdvancePaymentClearing
    {
        public string Row { get; set; }
        public string DocEntry { get; set; }
        public string DocNum { get; set; }
        public string RefNo { get; set; }
        public string JobNumber { get; set; }
        public string VendorCode { get; set; }
        public string VendorName { get; set; }
        public string IssueDate { get; set; }
        public double DocTotal { get; set; }
        public string Remarks { get; set; }
        public string PODocEntry { get; set; }
        public string Status { get; set; }
        public string IsUpdate { get; set; }
    }
}
