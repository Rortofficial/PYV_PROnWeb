namespace Client.Models.Request
{
    public class EmployeeClearingAdvanceRequest
    {
        public string CardCode { get; set; }
        public string SeriesCode { get; set; }
        public DateTime DocDate { get; set; }
        public string WebId { get; set; }
        public string TransId { get; set; }
        public double Total { get; set; }
        public int DocLine { get; set; }
        public List<EmployeeClearingAdvanceLine> Lines { get; set; }
    }
    public class EmployeeClearingAdvanceLine
    {
        public string CreditMethodCode { get; set; }
        public string PrefixCreditNo { get; set; }
        public string ValidUntil { get; set; }
        public string IdNo { get; set; }
        public string PayMethod { get; set; }
        public double AmountDue { get; set; }
        public string VoucherNo { get; set; }
    }
}
