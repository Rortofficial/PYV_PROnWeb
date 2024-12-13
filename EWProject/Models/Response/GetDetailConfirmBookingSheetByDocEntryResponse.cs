using Client.Models.Gets;

namespace Client.Models.Response
{
    public class GetDetailConfirmBookingSheetByDocEntryResponse
    {
        public string ErrorMessage { get; set; } = null!;
        public int ErrorCode { get; set; }
        public GetDetailConfirmBookingSheetByDocEntry Data { get; set; } = null!;
        public List<GetLayoutShowByType> ListLayoutPrint { get; set; } = null!;
    }
    public class GetGeneralListCode
    {
        public int Code { get; set; }
        public string Name { get; set; }
    }
    public class GetDetailConfirmBookingSheetByDocEntry
    {
        public string DocEntry { get; set; }
        public string JobType { get; set; }
        public string Route { get; set; }
        public string SaleName { get; set; }
        public List<GetGeneralListCode> Border { get; set; }
        public string JobNo { get; set; }
        public List<GetGeneralListCode> PlaceOfLoading { get; set; }
        public List<GetGeneralListCode> Shipper { get; set; }
        public List<GetGeneralListCode> PlaceOfDelivery { get; set; }
        public List<GetGeneralListCode> Consignee { get; set; }
        public string PriceList { get; set; }
        public string Remarks { get; set; }
        public string LoadingDate { get; set; }
        public string CrossBorderDate { get; set; }
        public string ETARequirementDate { get; set; }
        public string CSName { get; set; }
        public List<ListOfContainerInformation> ListOfContainerInformations { get; set; }
    }
    public class ListOfContainerInformation
    {
        public string Type { get; set; }
        public string ContainerOptionType { get; set; }
        public string ContainerType { get; set; }
        public string ContainerNo { get; set; }
        public string Owner { get; set; }
        public double GrossWeight { get; set; }
        public string Yard { get; set; }
        public string FCL { get; set; }
        public string LOLO { get; set; }
        public string TruckProvince { get; set; }
        public string TruckPlateNo { get; set; }
        public string TruckType { get; set; }
        public string Brand { get; set; }
        public string Colors { get; set; }
        public string TrailerProvince { get; set; }
        public string TrailerPlateNo { get; set; }
        public string TrailerType { get; set; }
        public string DriverName1 { get; set; }
        public string TPNo1 { get; set; }
        public string IDCard1 { get; set; }
        public string DriverLicense1 { get; set; }
        public string DriverName2 { get; set; }
        public string TPNo2 { get; set; }
        public string IDCard2 { get; set; }
        public string DriverLicense2 { get; set; }
        public string VendorNo { get; set; }
        public double TruckCostTHB { get; set; }
        public string SealNo1 { get; set; }
        public string SealNo2 { get; set; }
        public string ListInvoice { get; set; }
        public List<Models.Request.PurchaseRequest> listOfAdvancePayments { get; set; }
        public List<Models.Request.PurchaseRequest> listOfPurchaseRequests { get; set; }
    }
    #region AdvancePayment List
    public class ListOfAdvancePayment
    {
        public string AdvancePaymentID { get; set; }
        public string Series { get; set; }
        public string PostingDate { get; set; }
        public string Remarks { get; set; }
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public string Ref3 { get; set; }
        public string JobNo { get; set; }
        public List<ListLineAdvancePayment> Lines { get; set; }
    }
    public class ListLineAdvancePayment
    {
        public string EmployeeCodeAndEmployeeName { get; set; }
        public string AccountCodeAndAccountName { get; set; }
        public double Amount { get; set; }
    }
    #endregion End AdvancePayment List
    #region PurchaseRequest List
    public class ListOfPurchaseRequest
    {
        public string RefNo { get; set; }
        public string JobNo { get; set; }
        public string IssueBy { get; set; }
        public string IssueDate { get; set; }
        public string VendorName { get; set; }
        public string DueDate { get; set; }
        public string AmountCurrency { get; set; }
        public double THBAmount { get; set; }
        public double GrandTotal { get; set; }
        public string BankAccount { get; set; }
        public string Branch { get; set; }
        public string Country { get; set; }
        public string BankName { get; set; }
        public string SwiftCode { get; set; }
        public string Remark { get; set; }
        public List<ListItemLine> Lines { get; set; }
    }
    public class ListItemLine
    {
        public string Description { get; set; }
        public string ServiceType { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public double Amount { get; set; }
    }
    #endregion
}
