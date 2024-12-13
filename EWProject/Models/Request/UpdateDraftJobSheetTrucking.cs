using Client.Models.Gets;

namespace Client.Models.Request
{
    public class UpdateDraftJobSheetTrucking
    {
        public string CardCode { get; set; }
        public string CurrencySelling { get; set; }
        public string JobNo { get; set; }
        public int ConfirmBookingID { get; set; }
        public int DocEntry { get; set; }
        public string SaleQuotationDocNum { get; set; }
        public double DutyTaxAmountSelling { get; set; }
        public double TotalCosting { get; set; }
        public double REBATE { get; set; }
        public double GrandTotalCosting { get; set; }
        public double GrandTotalCostingUSD { get; set; }
        public double TotalSelling { get; set; }
        public double GrandTotalSelling { get; set; }
        public double GrandTotalSellingUSD { get; set; }
        public double Profit { get; set; }
        public string RemarkForCosting { get; set; }
        public string RemarkForSelling { get; set; }
        public string CostingConfirm { get; set; }
        public string UpdateBy { get; set; }
        public string CostCenter { get; set; }
        public List<JobSheetTruckingSelling> JobSheetTruckingSellings { get; set; }
        public List<JobSheetTruckingCosting> JobSheetTruckingCostings { get; set; }
        public List<Attachments> Attachments { get; set; }
    }
    public class JobSheetTruckingSelling
    {
        public int LineId { get; set; }
        public string ITEMCODE { get; set; }
        public double TOTALPRICE { get; set; }
        public int Qty { get; set; }
        public int ContainerType { get; set; }
        public string ItemType { get; set; }
        public string LineNum { get; set; }
    }
    public class JobSheetTruckingCosting
    {
        public int LineId { get; set; }
        public string ITEMCODE { get; set; }
        public int Qty { get; set; }
        public double TOTALPRICE { get; set; }
        public int ContainerType { get; set; }
    }
}
