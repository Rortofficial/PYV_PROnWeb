namespace Client.Models.Request
{
    public class ListAdvancePaymentConfirm
    {
        public ListAdvancePaymentParamater ListAdvanceParam { get; set; } = null!;
        public int ID { get; set; }
        public int VoucherNumber { get; set; }
        public string SeriesCode { get; set; } = null!;
        public DateTime DocDate { get; set; }
        public string Remarks { get; set; } = null!;
        public string Ref1 { get; set; } = null!;
        public string Ref2 { get; set; } = null!;
        public string Ref3 { get; set; } = null!;
        public string ProjectManagement { get; set; } = null!;
        public List<ListAdvancePaymentConfirmLine> Lines { get; set; } = null!;

    }
    public class ListAdvancePaymentConfirmLine
    {
        public int id { get; set; }
        public string AccountCode { get; set; } = null!;
        public string accountCodeOrAccountName { get; set; } = null!;
        public string BpCode { get; set; } = null!;
        public string bpCodeOrBpName { get; set; } = null!;
        public double amount { get; set; }
    }
    public class ListAdvancePaymentParamater
    {
        public string CountItemLine { get; set; } = null!;
        public string ListAdvance { get; set; } = null!;
        public string ListPRID { get; set; } = null!;
        public string ListAdvanceID { get; set; } = null!;
        public string AdvanceID { get; set; } = null!;
        public string ListPRTemplate { get; set; } = null!;
        public string ListADTemplate { get; set; } = null!;
    }
}
