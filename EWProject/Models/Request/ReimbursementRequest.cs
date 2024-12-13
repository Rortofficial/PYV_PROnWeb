namespace Client.Models.Request
{
    public class ReimbursementRequest
    {
        public int DocEntry { get; set; }
        public string DocStatus { get; set; }
        public string DocStatusUpdate { get; set; }
        public string AccountCode { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public double Amount { get; set; }
        public double AmountLimit { get; set; }
        public string Remark { get; set; }
        public string Reason { get; set; }
        public string UserID { get; set; }
    }
}
