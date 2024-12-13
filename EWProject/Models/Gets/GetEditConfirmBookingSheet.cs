namespace Client.Models.Gets
{
    public class GetEditConfirmBookingSheet
    {
        public int ConfirmBookingID { get; set; }
        public string Remark { get; set; }
        public int PriceList { get; set; }
        public string Type { get; set; }
        public string ContainerType { get; set; }
        public string ContainerNo { get; set; }
        public string Owner { get; set; }
        public string ContainerNo2 { get; set; }
        public string Owner2 { get; set; }
        public decimal GrossWeight { get; set; }
        public string Yard { get; set; }
        public string FCL_LCL { get; set; }
        public string LOLO_UNLOADING { get; set; }
        public string TruckProvince { get; set; }
        public string TruckPlateNo { get; set; }
        public string TruckType { get; set; }
        public string Brand { get; set; }
        public string TruckCode { get; set; }
        public string Color { get; set; }
        public string TrailerProvince { get; set; }
        public string TrailerPlate { get; set; }
        public string TrailerType { get; set; }
        public int Driver1 { get; set; }
        public string TPNO1 { get; set; }
        public string IDCard1 { get; set; }
        public string DriverLicense1 { get; set; }
        public int Driver2 { get; set; }
        public string TPNO2 { get; set; }
        public string IDCard2 { get; set; }
        public string DriverLicense2 { get; set; }
        public string VendorCode { get; set; }
        public decimal TruckCostTHB { get; set; }
        public string SealNo1 { get; set; }
        public string SealNo2 { get; set; }
        public string ContainerOption { get; set; }
        public int BookingLineId { get; set; }
        public int LineId { get; set; }
        public int BookingDocEntry { get; set; }
        public string ImportType { get; set; }
        public string Route { get; set; }
        public string SalePerson { get; set; }
        public string ThaiBorder { get; set; }
        public string JobNumber { get; set; }
        public string PlaceOfLoading { get; set; }
        public string Shipper { get; set; }
        public string PlaceOfDelivery { get; set; }
        public string Consignee { get; set; }
        public string CSName { get; set; }
        public string LoadingDate { get; set; }
        public string CrossBorderDate { get; set; }
        public string ETARequirementDate { get; set; }
        public string Destination { get; set; }
    }
}
