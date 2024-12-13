namespace Client.Models.Request
{
    public class ListPurchaseRequestConfirm
    {
        public ListPurchaseRequestParamater ListPRParam { get; set; } = null!;
        public int ID { get; set; }
        public string RefNo { get; set; } = null!;
        public string JobNo { get; set; } = null!;
        public string IssueDate { get; set; } = null!;
        public string IssueBy { get; set; } = null!;
        public string VendorCode { get; set; } = null!;
        public string VendorName { get; set; } = null!;
        public string DueDate { get; set; } = null!;
        public string AmountCurrency { get; set; } = null!;
        public decimal GrandTotal { get; set; } = 0;
        public decimal AmountTHB { get; set; } = 0;
        public string Remarks { get; set; } = null!;
        public string BankAccount { get; set; } = null!;
        public string BranchName { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string BankName { get; set; } = null!;
        public string SwiftCode { get; set; } = null!;
        public List<ListPurchaseRequestConfirmLine> Lines { get; set; } = null!;
    }
    public class ListPurchaseRequestConfirmLine
    {
        public int ID { get; set; }
        public string ItemCode { get; set; } = null!;
        public string ItemName { get; set; } = null!;
        public string ServiceType { get; set; } = null!;
        public string Origin { get; set; } = null!;
        public string Destination { get; set; } = null!;
        public string amount { get; set; }
        public string Remark { get; set; }
        public string DistributionRule { get; set; }
        public string VatCode { get; set; }
        public string ServiceTypeID { get; set; }
        public string ServiceTypeName { get; set; }
        public string RefInv { get; set; }
    }
    public class ListPurchaseRequestParamater
    {
        public string ListPR { get; set; } = null!;
        public string ListAD { get; set; }
        public string ListPRID { get; set; } = null!;
        public string ListAdvanceID { get; set; } = null!;
        public int CountItemLine { get; set; }
        public string PRID { get; set; } = null!;
        public string ListPRTemplate { get; set; } = null!;
        public string ListADTemplate { get; set; } = null!;
    }
}
