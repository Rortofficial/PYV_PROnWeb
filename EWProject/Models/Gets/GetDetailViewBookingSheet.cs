using Client.Models.Request;

namespace Client.Models.Gets
{
    public class GetDetailViewBookingSheet
    {
        public string DocEntry { get; set; } = null!;
        public string BookingId { get; set; } = null!;
        public string JobNo { get; set; } = null!;
        public string BookingDate { get; set; } = null!;
        public string RouteFrom { get; set; } = null!;
        public string RouteTo { get; set; } = null!;
        public string SaleEmployee { get; set; } = null!;
        public string JobType { get; set; } = null!;
        public string ServiceType { get; set; } = null!;
        public string Shipper { get; set; } = null!;
        public string Consignee { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string TotalPackage { get; set; } = null!;
        public string NetWeight { get; set; } = null!;
        public string GrossWeight { get; set; } = null!;
        public string LoadingDate { get; set; } = null!;
        public string CrossBorderDate { get; set; } = null!;
        public string ETARequirementDate { get; set; } = null!;
        public string ThaiForwarder { get; set; } = null!;
        public string TruckType { get; set; } = null!;
        public string Remark { get; set; } = null!;
        public string SpecialRequest { get; set; } = null!;
        public List<Commodity> Commodities { get; set; } = null!;//GetInvoice
        public List<PlaceOfLoading> PlaceOfLoadings { get; set; } = null!; //PLACEOFLOADINGBYDOCENTRY
        public List<PlaceOfDelivery> placeOfDeliveries { get; set; } = null!; //PLACEOFDELIVERYBYDOCENTRY
        public List<Volume> Volumes { get; set; } = null!;//VOLUMEBYDOCENTRY
        public List<ThaiBorders> ThaiBorders { get; set; } = null!;//THAIBORDERBYDOCENTRY
        public List<string> OverseaTruckers { get; set; } = null!;//OverseaTruckersByDocEntry
        public List<string> OverseaForwarders { get; set; } = null!;//OverseaForwardersByDocEntry
        public List<string> SaleQuotations { get; set; } = null!;//SaleQuotationsByDocEntry
    }
}
