namespace Client.Models.Gets
{
    public class ListOfApproveLoadingAndDelivery
    {
        public string DocEntry { get; set; }
        public int BookingID { get; set; }
        public string JobNumber { get; set; }
        public string PlaceOfLoading { get; set; }
        public string LoadingOfDistrict { get; set; }
        public string PlaceOfDelivery { get; set; }
        public string DeliveryOfDistrict { get; set; }
        public string Status { get; set; }
        public string PlaceOfLoadingCurrent { get; set; }
        public string PlaceOfDeliveryCurrent { get; set; }
        public string LoadingDistrictCurrenct { get; set; }
        public string DeliveryDistrictCurrenct { get; set; }
        public string CreateBy { get; set; }
    }
}
