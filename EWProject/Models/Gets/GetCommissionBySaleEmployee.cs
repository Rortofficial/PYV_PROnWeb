namespace Client.Models.Gets
{
    public class GetCommissionBySaleEmployee
    {
        public string JobNo { get; set; }
        public int SONo { get; set; }
        public int DocNum { get; set; }
        public string DocStatus { get; set; }
        public decimal GrandTotalCosting { get; set; }
        public decimal GrandTotalSelling { get; set; }
        public decimal TotalPayment { get; set; }
        public decimal GrossProfit { get; set; }
        public decimal Commission { get; set; }
        public decimal TotalCommission { get; set; }
        public decimal ARBalance { get; set; }
        public int SaleEmployee { get; set; }
        public int ARInvoice { get; set; }
        public string IncomingNumber { get; set; }
        public string Consignee { get; set; }
        public string CustomerName { get; set; }
        public string Shipper { get; set; }
        public double Rebate { get; set; }
    }
}
