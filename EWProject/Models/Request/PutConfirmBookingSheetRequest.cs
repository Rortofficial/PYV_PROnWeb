namespace Client.Models.Request
{
    public class PutConfirmBookingSheetRequest
    {
        public int ConfirmBookingID { get; set; }
        public string CreateUser { get; set; }
        public string CSName { get; set; }
        public string Remarks { get; set; }
        public string PriceList { get; set; }
        public string Destination { get; set; }
        public List<UpdateTruckInformationRequest> Lines { get; set; } = null!;
    }
    public class UpdateTruckInformationRequest
    {
        public int LineId { get; set; }
        public string TYPE { get; set; }
        public string CONTAINERTYPE { get; set; }
        public string CONTAINERNO { get; set; }
        public string OWNER { get; set; }
        public string GROSSWEIGHT { get; set; }
        public string YARD { get; set; }
        public string FCL_LCL { get; set; }
        public string LOLO_UNLOADING { get; set; }
        public string TRUCKPROVINCE { get; set; }
        public string TRUCKPLATENO { get; set; }
        public string TRUCKTYPE { get; set; }
        public string BRAND { get; set; }
        public string TRUCKCODE { get; set; }
        public string COLOR { get; set; }
        public string TRAILERPROVINCE { get; set; }
        public string TRAILERPLATE { get; set; }
        public string TRAILERTYPE { get; set; }
        public string DRIVER1 { get; set; }
        public string TPNO1 { get; set; }
        public string IDCARD1 { get; set; }
        public string DRIVERLICENSE1 { get; set; }
        public string DRIVER2 { get; set; }
        public string TPNO2 { get; set; }
        public string IDCARD2 { get; set; }
        public string DRIVERLICENSE2 { get; set; }
        public string VENDORCODE { get; set; }
        public string TRUCKCOSTTHB { get; set; }
        public string SEALNO1 { get; set; }
        public string SEALNO2 { get; set; }
        public string CONTAINERNO2 { get; set; }
        public string ExchangeType { get; set; }
        public string OWNER2 { get; set; }
        public string ContainerOption { get; set; }
        public string BookingLineId { get; set; }
    }
}
