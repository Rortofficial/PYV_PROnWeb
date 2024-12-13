namespace Client.Models.Gets
{
    public class GetDetailBookingSheetByUser
    {
        public int BookingID { get; set; }
        public string ImportType { get; set; } = null!;
        public string SaleEmployee { get; set; } = null!;
        public string Route { get; set; } = null!;
        public string JobNo { get; set; } = null!;
        public string Origin { get; set; } = null!;
        public string Destination { get; set; } = null!;
        public string LOLOYARDRemark { get; set; }
        public string LOLOUNLOADING { get; set; }
        public string FCLORLCL { get; set; }
        public int SlpCode { get; set; }
        public List<string> Shipper { get; set; } = null!;
        public List<string> Consignee { get; set; } = null!;
        public string CO { get; set; } = null!;
        public List<string> Invoices { get; set; } = null!;
        public List<string> PlaceOfDelivery { get; set; } = null!;
        public List<string> PlaceOfDeliveryDistrict { get; set; } = null!;
        public List<string> PlaceOfLoading { get; set; } = null!;
        public List<string> PlaceOfLoadingDistrict { get; set; } = null!;
        public List<BookingVolume> Volume { get; set; } = null!;
        public List<string> ThaiBorder { get; set; } = null!;
    }
    public class BookingVolume
    {
        public double Qty { get; set; }
        public string VoulumeCode { get; set; } = null!;
        public string VolumeDescription { get; set; } = null!;
        public int ContainerNumber { get; set; }
        public int DocEntry { get; set; }
        public double GrossWeight { get; set; }
        public string Type { get; set; }
        public string VolumeInvoiceList { get; set; }
        public int LineId { get; set; }
        public int ConfirmBookingID { get; set; }
    }
}
