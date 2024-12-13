using Client.Models.Gets;

namespace Client.Models.Request
{
    public class PostJobSheetTruckingRequest
    {
        public int DocEntry { get; set; }
        public int DocNum { get; set; }
        public string ConfirmCostingButton { get; set; }
        public int ConfirmBookingID { get; set; }
        public string CardCode { get; set; } = null!;
        public string SaleQuotationDocNum { get; set; } = null!;
        public string TuckWayBillBy { get; set; } = null!;
        public string CurrencyCosting { get; set; } = null!;
        public string CurrencySelling { get; set; } = null!;
        public string RemarksCosting { get; set; } = null!;
        public string RemarksSelling { get; set; } = null!;
        public double TotalCosting { get; set; }
        public double Rebate { get; set; }
        public double GrandTotalCosting { get; set; }
        public double GrandTotalCostingUSD { get; set; }
        public double TotalSelling { get; set; }
        public double GrandTotalSelling { get; set; }
        public double GrandTotalSellingUSD { get; set; }
        public double DutyTaxAmountCosting { get; set; }
        public double DutyTaxAmountSelling { get; set; }
        public double AdvanceAmountCosting { get; set; }
        public double AdvanceAmountSelling { get; set; }
        public double TotalProfit { get; set; }
        public string UserCreate { get; set; }
        public string UpdateBy { get; set; }
        public string JobNo { get; set; } = null!;
        public string CardName { get; set; }
        public string SQREF { get; set; }
        public string CostCenter { get; set; }
        //public List<JobSheetRuckingLineForSelling> ItemSellLines { get; set; } = null!;
        //public List<JobSheetRuckingLineForCosting> ItemCostLines { get; set; } = null!;
        public List<JobSheetRuckingLineForItem> ItemLine { get; set; }
        public List<Attachments> Attachment { get; set; }
    }
    public class JobSheetRuckingLineForItem
    {
        public string itemType { get; set; }
        public int lineNum { get; set; }
        public string itemCode { get; set; }
        public string itemName { get; set; }
        public int UomCode { get; set; }
        public string UomName { get; set; }
        public double priceCosting { get; set; }
        public double priceSelling { get; set; }
        public double qtyCosting { get; set; }
        public double qtySelling { get; set; }
        public int LineId { get; set; }
    }
    public class JobSheetRuckingLineForCosting
    {
        public string ItemCode { get; set; } = null!;
        public double Qty { get; set; }
        public double Price { get; set; }
    }
}
