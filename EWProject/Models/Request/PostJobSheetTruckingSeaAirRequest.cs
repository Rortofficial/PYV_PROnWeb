using Client.Models.Gets;

namespace Client.Models.Request
{
    public class PostJobSheetTruckingSeaAirRequest
    {
        public PostJobSheetTruckingSeaAir ObjectHeader { get; set; }
        public List<JobSheetRuckingLineForItemSeaAir> ItemLineRevenue { get; set; }
        public List<JobSheetRuckingLineForItemCostingSeaAir> ItemLineCosting { get; set; }
        public List<Attachments> Attachment { get; set; }
    }
    public class JobSheetRuckingLineForItemSeaAir
    {
        public int LineId { get; set; }
        public string ITEMCODE { get; set; }
        public double TOTALPRICE { get; set; }
        public double Qty { get; set; }
        public string ContainerType { get; set; }
        public string Currency { get; set; }
        public double ExchangeRate { get; set; }
        public double TotalBaht { get; set; }
        public string RefLineId { get; set; }
    }
    public class JobSheetRuckingLineForItemCostingSeaAir
    {
        public int LineId { get; set; }
        public string ITEMCODE { get; set; }
        public double TOTALPRICE { get; set; }
        public double Qty { get; set; }
        public string ContainerType { get; set; }
        public string Currency { get; set; }
        public double ExchangeRate { get; set; }
        public double TotalBaht { get; set; }
        public string RefLineId { get; set; }
        public string VendorCode { get; set; }
        public string PurchaseOrder { get; set; }
    }
    public class PostJobSheetTruckingSeaAir
    {
        //public string DocEntry { get; set; }
        public string CONFIRMBOOKINGID { get; set; }
        public string SALESORDERDOCNUM { get; set; }
        public string PurchaseOrder { get; set; }
        public string CARDCODE { get; set; }
        //public string CARDNAME { get; set; }
        public string VendorCode { get; set; }
        public string REMARKSCOSTING { get; set; }
        public string REMAKRSSELLING { get; set; }
        public double TOTALCOSTING { get; set; }
        public double REBATE { get; set; }
        public double GRANDTOTALCOSTING { get; set; }
        public double TOTALSELLING { get; set; }
        public double GRANDTOTALSELLING { get; set; }
        public double DutyTaxAmountCosting { get; set; }
        public double DutyTaxAmountSelling { get; set; }
        public double TOTALPROFIT { get; set; }
        public string USERCREATE { get; set; }
        public string JOBNO { get; set; } = null!;
        public string COSTINGCONFIRM { get; set; }
    }
}
