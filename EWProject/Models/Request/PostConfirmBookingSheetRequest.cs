namespace Client.Models.Request
{
    public class PostConfirmBookingSheetRequest
    {
        public int DocEntry { get; set; }
        public int BookingID { get; set; }
        public string CreateUser { get; set; }
        public string CSName { get; set; }
        public string Remarks { get; set; }
        public string CardCode { get; set; }
        public string PriceList { get; set; }
        public string SlpCode { get; set; }
        public string Destination { get; set; }
        public List<TruckInformationRequest> Lines { get; set; } = null!;
    }
    public class TruckInformationRequest
    {
        public string Type { get; set; }
        public string ContainerType { get; set; }
        public string ContainerNo { get; set; }
        public string Owner { get; set; }
        public string ContainerNo2 { get; set; }
        public string ExchangeType { get; set; }
        public string Owner2 { get; set; }
        public string GrossWeight { get; set; }
        public string Yard { get; set; }
        public string FCL_LCL { get; set; }
        public string LOLO_Unloading { get; set; }
        public string TruckProvince { get; set; }
        public string TruckPlateNo { get; set; }
        public string TruckType { get; set; }
        public string Brand { get; set; }
        public string TruckCode { get; set; }
        public string Color { get; set; }
        public string TrailerProvince { get; set; }
        public string TrailerPlate { get; set; }
        public string TrailerType { get; set; }
        public string DriverCode1 { get; set; }
        public string TPNo1 { get; set; }
        public string IDCard1 { get; set; }
        public string DriverLicense1 { get; set; }
        public string DriverCode2 { get; set; }
        public string TPNo2 { get; set; }
        public string IDCard2 { get; set; }
        public string DriverLicense2 { get; set; }
        public string VendorCode { get; set; }
        public double TruckCostTHB { get; set; }
        public string SealNo1 { get; set; }
        public string SealNo2 { get; set; }
        public string ContainerOption { get; set; }
        public string BookingLineId { get; set; }
        //public List<AdvancePayment> ListAdvancePayment { get; set; }
        public List<PurchaseRequest> ListAdvancePayment { get; set; }
        public List<PurchaseRequest> ListPurchaseRequest { get; set; }
    }
    public class AdvancePayment
    {
        public DateTime DocDate { get; set; }
        public string ID { get; set; }
        public string ProjectManagement { get; set; }
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public string Ref3 { get; set; }
        public string Remarks { get; set; }
        public string SeriesCode { get; set; }
        public string VoucherNumber { get; set; }
        public List<AdvancePaymentLine> Lines { get; set; }
        public string DocEntry { get; set; }
    }
    public class AdvancePaymentLine
    {
        public string AccountCode { get; set; }
        public string BpCode { get; set; }
        public string accountCodeOrAccountName { get; set; }
        public double amount { get; set; }
        public string bpCodeOrBpName { get; set; }
    }
    public class PurchaseRequest
    {
        public string Series { get; set; }
        public int ID { get; set; }
        public string DocStatus { get; set; } = string.Empty;
        public string RefNo { get; set; } = null!;
        public string JobNo { get; set; } = string.Empty;
        public string IssueDate { get; set; }
        public int IssueBy { get; set; }
        public string IssueByName { get; set; }
        public string VendorCode { get; set; }= string.Empty;
        public string VendorName { get; set; }
        public string DueDate { get; set; }
        public string AmountCurrency { get; set; } = string.Empty;
        public decimal TotalPaid { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal AmountTHB { get; set; }
        public string Remarks { get; set; } = string.Empty;
        public string BankAccount { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string BankName { get; set; } = string.Empty;
        public string SwiftCode { get; set; } = string.Empty;
        public string ArInvoice { get; set; } = string.Empty;
        public List<PurchaseRequestLine> Lines { get; set; }
        public List<string> ListJobSheetTruckByDocEntry { get; set; }
        public List<string> ListConfirmBookingByDocEntry { get; set; }
        public string DocEntry { get; set; }
        public int LineNumBase { get; set; }
        public string PRDocEntry { get; set; }
        public string PRDocNum { get; set; }
        public string DocNum { get; set; }
        public string Reason { get; set; }
        public int MaxLineNum { get; set; }
        public int DocEntryPurchaseRequest { get; set; }
        public string PRType { get; set; }
        public string ConfirmBookingID { get; set; }
    }
    public class PurchaseRequestLine
    {
        public int LineNumPO { get; set; }
        public int LineNumAD { get; set; }
        public int LineNumPR { get; set; }
        public int LineNumber { get; set; }
        public int ID { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ServiceType { get; set; }
        public string ServiceTypeID { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public decimal Amount { get; set; }
        public string JobNo { get; set; }
        public decimal Paid { get; set; }
        public string remark { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public string RefInv { get; set; }
        public string TransportationNo { get; set; }
        public string DistributionRule { get; set; }
        public string VatCode { get; set; }
        public double VatRate { get; set; }
        public string TruckNumber { get; set; }
        public string BaseLineID { get; set; }
        public string PlaceOfDelivery { get; set; }
        public string PlaceOfLoading { get; set; }
        public string TrailerType { get; set; }
        public string TruckType { get; set; }
        public string LoadingDate { get; set; }
    }
}
