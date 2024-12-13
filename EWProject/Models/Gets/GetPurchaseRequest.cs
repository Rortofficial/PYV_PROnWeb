namespace Client.Models.Gets
{
    public class GetPurchaseRequest
    {
        public string DocEntry { get; set; }
        public string DocNum { get; set; }
        public string Status { get; set; }
        public string UserRequest { get; set; }
        public string UpdateBy { get; set; }
        public string IssueDate { get; set; }
        public string RequireDate { get; set; }
        public string Remark { get; set; }
        public string Reason { get; set; }
        public double DocTotal { get; set; }
        public string ProjectNumber { get; set; }
        public string DocNumPR { get; set; }
        public string VendorCode { get; set; }
        public string VendorName { get; set; }
        public string UpdateStatus { get; set; }
        public string CancelStatus { get; set; }
        public string UpdateTime { get; set; }
        public string CreateTime { get; set; }
        public string CreateDate { get; set; }
        public string UpdateDate { get; set; }
    }
}
