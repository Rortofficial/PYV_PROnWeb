namespace Client.Models.Gets
{
    public class GetListDocumentApproveByType
    {
        public string DocEntry { get; set; }
        public string DocNum { get; set; }
        public string DocNumPR { get; set; }
        public string DocumentType { get; set; }
        public string ProjectNumber { get; set; }
        public string IssueDate { get; set; }
        public string VendorCode { get; set; }
        public string VendorName { get; set; }
        public string Currency { get; set; }
        public string UserID { get; set; }
        public string EmployeeName { get; set; }
        public double Amount { get; set; }
        public string Remarks { get; set; }
        public string Type { get; set; }
        public string ApproveBy { get; set; }
    }
}
