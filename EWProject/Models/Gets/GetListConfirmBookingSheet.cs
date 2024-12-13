namespace Client.Models.Gets
{
    public class GetListConfirmBookingSheet
    {
        public int ConfirmBookingID { get; set; }
        public string JobNo { get; set; } = null!;
        public string BookingDate { get; set; }
        public string UpdateBy { get; set; }
        public string UPDATEDATE { get; set; } = null!;
        public string CREATETIME { get; set; } = null!;
        public string UPDATETIME { get; set; } = null!;
        public string LoadingDate { get; set; }
        public string CrossBorderDate { get; set; }
        public string ETARequirementDate { get; set; }
        public string ImportType { get; set; } = null!;
        public string Shipper { get; set; } = null!;
        public string Consignee { get; set; } = null!;
        public string CO { get; set; } = null!;
        public string Route { get; set; } = null!;
        public string CreateBy { get; set; } = null!;
        public string DocStatus { get; set; }
        public string DocStatusUpdate { get; set; }
        public int DocEntry { get; set; }
        public int BookingDocEntry { get; set; }
        public int ProjectDocEntry { get; set; }
        public string Commodities { get; set; }
    }
}
