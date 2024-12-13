using Client.Models.Gets;

namespace Client.Models.Response
{
    public class ListMasterConfirmBookingSheet
    {
        public List<GetBookingSheetByUser> getBookingSheetByUsers { get; set; }
        public List<GetDetailBookingSheetByUser> getDetailBookingSheetByUsers { get; set; }
        public List<GetContainer> getContainers { get; set; }
        public List<GetVendor> getVendors { get; set; }
        public List<GetTruckNo> getTruckNos { get; set; }
        public List<GetTrailerNo> getTrailerNos { get; set; }
        public List<GetEmployee> getEmployees { get; set; }
        public List<GetCostType> getCostTypes { get; set; }
        public List<GetBookingSheetByUser> getBookingSheetByUser { get; set; }
        public List<GetPriceListConfirmBooking> getPriceListConfirmBookings { get; set; }
        public List<GetTruckProvince> getTruckProvinces { get; set; }
        public List<GetEditConfirmBookingSheet> GetEditConfirmBookingSheets { get; set; }
        public List<CSName> GetCSNames { get; set; }
    }
}
