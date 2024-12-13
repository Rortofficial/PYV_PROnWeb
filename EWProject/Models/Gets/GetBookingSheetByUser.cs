namespace Client.Models.Gets
{
    public class GetBookingSheetByUser
    {
        public string DocEntry { get; set; } = null!;
        public string BookingID { get; set; } = null!;
        public string JobNo { get; set; } = null!;
        public string BookingDate { get; set; } = null!;
        public string UPDATEDATE { get; set; } = null!;
        public string CREATETIME { get; set; } = null!;
        public string UPDATETIME { get; set; } = null!;
        public string LoadingDate { get; set; } = null!;
        public string CrossBorderDate { get; set; } = null!;
        public string ETARequirementDate { get; set; } = null!;
        public string ImportType { get; set; } = null!;
        public string Volumn { get; set; }
        public string ContainerType { get; set; }
        public string Shipper { get; set; } = null!;
        public string Consignee { get; set; } = null!;
        public string CO { get; set; } = null!;
        public string COCode { get; set; }
        public string Route { get; set; } = null!;
        public string DocStatus { get; set; } = null!;
        public string DocStatusCancel { get; set; }
        public string CreateBy { get; set; } = null!;
        public string UpdateBy { get; set; } = null!;
        public string SaleEmployee { get; set; } = null!;
        public int SlpCode { get; set; }
        public string StatusUpdate { get; set; }
        public string Destination { get; set; }
        public string Origin { get; set; }
        public string COMMODITYS { get; set; }
        public string SpecialRequest { get; set; }
    }
}
