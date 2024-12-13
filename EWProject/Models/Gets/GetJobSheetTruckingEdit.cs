namespace Client.Models.Gets
{
    public class GetJobSheetTruckingEdit
    {
        public int DocEntry { get; set; }
        public int DocNum { get; set; }
        public string SalesPerson { get; set; }
        public string CurrencyCosting { get; set; }
        public string CurrencySelling { get; set; }
        public decimal TotalCosting { get; set; }
        public decimal TotalSelling { get; set; }
        public decimal Rebate { get; set; }
        public decimal GrandTotalCosting { get; set; }
        public decimal GrandTotalSelling { get; set; }
        public decimal GrandTotalCostingUSD { get; set; }
        public decimal GrandTotalSellingUSD { get; set; }
        public decimal Profit { get; set; }
        public string RemarkForCosting { get; set; }
        public string RemarkForSelling { get; set; }
        public decimal ExchangeRate { get; set; }
        public string CostingConfirm { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public List<Attachments> Attachments { get; set; }
        public List<GetJobSheetTruckingLineView> LinesCosting { get; set; }
        public List<GetJobSheetTruckingLineEdit> LinesCostingEdit { get; set; }
        public List<GetJobSheetTruckingLineView> LinesSelling { get; set; }
    }
    public class GetJobSheetTruckingLineView
    {
        public string ITEMCODE { get; set; }
        public string ITEMNAME { get; set; }
        public decimal TOTALPRICE { get; set; }
        public decimal Qty { get; set; }
        public string UomName { get; set; }
        public int LineNumber { get; set; }
    }
    public class GetJobSheetTruckingLineEdit
    {
        public int LineId { get; set; }
        public string ITEMCODE { get; set; }
        public double TOTALPRICE { get; set; }
        public double Qty { get; set; }
    }
    public class Attachments
    {
        public int LineId { get; set; }
        public string ATTACHMENTNAME { get; set; }
    }
}
