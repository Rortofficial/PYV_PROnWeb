namespace Client.Models.Gets
{
    public class GetAdvancePayment
    {
        public string DocEntry { get; set; }
        public string DocNum { get; set; }
        public string Status { get; set; }
        public string StatusApprove { get; set; }
        public string DocDate { get; set; }
        public string Remark { get; set; }
        public double Total { get; set; }
    }
}
