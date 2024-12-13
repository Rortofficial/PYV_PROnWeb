namespace Client.Models.Request
{
    public class ListApproveReimbursement
    {
        public string ApproveType { get; set; }
        public string DocEntry { get; set; }
        public string AccountCode { get; set; }
        public string Reason { get; set; }
        public string UserID { get; set; }
        public string CardCode { get; set; }
    }
}
