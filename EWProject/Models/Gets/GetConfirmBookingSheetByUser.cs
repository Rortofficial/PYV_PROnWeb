namespace Client.Models.Gets
{
    public class GetConfirmBookingSheetByUser
    {
        public string DocEntry { get; set; } = null!;
        public string BookingID { get; set; } = null!;
        public string JobNo { get; set; } = null!;
        public string BookingDate { get; set; }
        public string ImportType { get; set; } = null!;
        public string CO { get; set; } = null!;
        public string Route { get; set; } = null!;
        public string DocStatus { get; set; } = null!;
        public string CreateBy { get; set; } = null!;
    }
}
