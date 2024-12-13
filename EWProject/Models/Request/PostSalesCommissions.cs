namespace Client.Models.Request
{
    public class PostSalesCommissions
    {
        public int DocEntry { get; set; }
        public string DocNum { get; set; }
        public string ApproveStatus { get; set; }
        public string IsCancel { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Reason { get; set; }
        public int SlpCode { get; set; }
        public string SlpName { get; set; }
        public string WebId { get; set; }
        public string VendorCode { get; set; }
        public string IssueDate { get; set; }
        public double GrandTotalCosting { get; set; }
        public double GrandTotalSelling { get; set; }
        public double GrandTotalGrossProfit { get; set; }
        public double GrandTotalCommission { get; set; }
        public string Currency { get; set; }
        public string Remark { get; set; }
        public string NumAtCard { get; set; }
        public string DueDate { get; set; }
        public List<PostSalesCommissionsRow> Lines { get; set; }
    }
    public class PostSalesCommissionsRow
    {
        public string AccountCode { get; set; }
        public string JobNumber { get; set; }
        public string ARDocEntry { get; set; }
        public string ARDocNum { get; set; }
        public double GrandTotalCosting { get; set; }
        public double GrandTotalSelling { get; set; }
        public double GrossProfit { get; set; }
        public double Percentage { get; set; }
        public double ARBalance { get; set; }
        public double TotalSaleCommission { get; set; }
        public int LineNumPO { get; set; }
        public string IncomingNumber { get; set; }
    }
}
