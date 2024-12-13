namespace Client.Models.Request
{
    public class EditPurchaseRequest
    {
        public EditPurchaseHeader Header { get; set; }
        public List<EditPurchaseRow> EditPurchaseRows { get; set; }
        public List<int> ListOfBaseLine { get; set; }
    }
    public class EditPurchaseHeader
    {
        //public string NumAtCard { get; set; }
        public string Project { get; set; }
        public string UserID { get; set; }
        public string IssueDate { get; set; }
        public string DueDate { get; set; }
        public string PRSeries { get; set; }
        public string VendorCode { get; set; }
        public double AmountTHB { get; set; }
        public double Amount { get; set; }
        public string CurrencyType { get; set; }
        public string UpdateBy { get; set; }
    }
    public class EditPurchaseRow
    {
        public int LineId { get; set; }
        public string ItemCode { get; set; }
        public int Qty { get; set; }
        public double Price { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string Remarks { get; set; }
        public string PriceList { get; set; }
        public string DistributionRule { get; set; }
        public string RefInv { get; set; }
        public string JobNumber { get; set; }
        public string ContainerType { get; set; }
        public string TaxCode { get; set; }
        public string TruckNo { get; set; }
    }

}
