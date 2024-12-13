namespace Client.Models.Gets
{
    public class GetListSalesCommission
    {
        public int DocEntry { get; set; }
        public string PRDocNum { get; set; }
        public string ApproveStatus { get; set; }
        public string DocumentCancel { get; set; }
        public int DocNum { get; set; }
        public string SaleEmployee { get; set; }
        public string Reason { get; set; }
        public string UserCreated { get; set; }
        public string IssueDate { get; set; }
        public string UpdateBy { get; set; }
        public double TotalSalesComission { get; set; }
    }
}
