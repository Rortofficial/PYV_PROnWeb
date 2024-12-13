namespace Client.Models.Gets
{
    public class GetJobSheetTruckingEditDraftByDocEntry
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
        public List<GetJobSheetTruckingEditDraftLine> ItemLine { get; set; }
        public List<Attachments> Attachment { get; set; }
    }
    public class GetJobSheetTruckingEditDraftLine
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public double PriceCosting { get; set; }
        public double PriceSelling { get; set; }
        public int QtyCosting { get; set; }
        public int QtySelling { get; set; }
        public int LineIndex { get; set; }
        public string UomCodeList { get; set; }
        public string UomCode { get; set; }
        public string LineNum { get; set; }
        public string ItemType { get; set; }
        public List<GetUomGroup> UomGroupList { get; set; }
    }
}
