namespace Client.Models.Request
{
    public class PostBookingSheetRequest
    {
        public int DocEntry { get; set; }
        public string JobNumber { get; set; }
        public int BookingID { get; set; }
        public string CreateUser { get; set; }
        public string EWSereis { get; set; }
        public List<Shippers> Shipper { get; set; }
        //public string CO { get; set; } = null!;
        public List<Consignees> Consignee { get; set; }
        public int SaleEmployee { get; set; }
        public string ImportType { get; set; }
        public string BookingDate { get; set; }
        public int Origin { get; set; }
        public int Destination { get; set; }
        public string GoodDescription { get; set; }
        public string TotalPackage { get; set; }
        public double NetWeight { get; set; }
        public double GrossWeight { get; set; }
        public string LoadingDate { get; set; }
        public string CorssBorderDate { get; set; }
        public string ETARequirement { get; set; }
        public string ThaiTrucker { get; set; }
        public List<OverTruckers> OverseaTrucker { get; set; }
        public int LoloYardOrUnloading { get; set; }
        public string CO { get; set; }
        //public string ThaiForwarder { get; set; } = null!;
        public List<OverForwarders> OverseaForwarder { get; set; }
        public int LCLOrFCL { get; set; }
        public int CYOrCFS { get; set; }
        public string LOLOYARDRemark { get; set; }
        public List<TruckType> TruckType { get; set; }
        public string SpeacialRequest { get; set; }
        public string Remarks { get; set; }
        public string ServiceType { get; set; }
        public List<SaleQuotations> SaleQuotation { get; set; }
        public List<ThaiForwarders> ThaiForwarder { get; set; }
        public List<Commodity> Commodities { get; set; }
        public List<PlaceOfLoading> PlaceOfLoadings { get; set; }
        public List<PlaceOfDelivery> PlaceOfDeliveries { get; set; }
        public List<Volume> Volumes { get; set; }
        public List<ThaiBorders> ThaiBorders { get; set; }
        public string DG { get; set; }
        public string OtherRemark { get; set; }
    }
    public class Consignees
    {
        public int LineId { get; set; }
        public string CONSIGNEE { get; set; }
    }
    public class Shippers
    {
        public int LineId { get; set; }
        public string SHIPPER { get; set; }
    }
    public class ThaiForwarders
    {
        public int LineId { get; set; }
        public string THAIFORWARDER { get; set; }
    }
    public class SaleQuotations
    {
        public int LineId { get; set; }
        public string DOCENTRY { get; set; }
        //public string CardCode { get; set; }
    }
    public class OverForwarders
    {
        public int LineId { get; set; }
        public string OVERSEAFORWARDERCODE { get; set; }
    }
    public class OverTruckers
    {
        public int LineId { get; set; }
        public string OVERSEATRUCKERCODE { get; set; }
    }
    public class Commodity
    {
        public int LineId { get; set; }
        public string INVOICE { get; set; } = null!;
    }
    public class PlaceOfLoading
    {
        public int LineId { get; set; }
        public string PLACELOADING { get; set; }
        public string District { get; set; }
    }
    public class PlaceOfDelivery
    {
        public int LineId { get; set; }
        public string PLACEDELIVERY { get; set; } = null!;
        public string District { get; set; }
    }
    public class Volume
    {
        public int LineId { get; set; }
        public int QTY { get; set; }
        public string VOLUMECODE { get; set; } = null!;
        public double GROSSWEIGHT { get; set; }
        public string InvoiceList { get; set; }
    }
    public class ThaiBorders
    {
        public int LineId { get; set; }
        public string ThaiBorder { get; set; } = null!;
    }
    public class TruckType
    {
        public int LineId { get; set; }
        public string QTY { get; set; }
        public string TRUCKTYPE { get; set; }
        public double GROSSWEIGHT { get; set; }
        public string InvoiceList { get; set; }
    }
}
