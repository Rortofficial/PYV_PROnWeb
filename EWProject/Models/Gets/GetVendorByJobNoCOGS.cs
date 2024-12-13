namespace Client.Models.Gets
{
    public class GetVendorByJobNoCOGS
    {
        public string VendorCode { get; set; }
        public string VendorName { get; set; }
        public string ConfirmBookingID { get; set; }
        public List<GetItemByJobNo> ItemByJobNo { get; set; }
    }
    public class GetItemByJobNo
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string SERVICETYPE { get; set; }
        public double Amount { get; set; }
        public int LineNumber { get; set; }
        public string TruckNumber { get; set; }
        public string TruckName { get; set; }
    }
}
