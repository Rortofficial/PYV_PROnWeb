using Client.Models.Gets;
using Client.Models.Request;

namespace Client.Models.Response
{
    public class GetDetailBookingSheetByDocEntryResponse
    {
        public string ErrorMessage { get; set; } = null!;
        public int ErrorCode { get; set; }
        public GetDetailBookingSheetByDocEntry Data { get; set; } = null!;
        public List<GetLayoutShowByType> ListLayoutPrint { get; set; } = null!;
    }
    public class GetDetailBookingSheetByDocEntry
    {
        public string ServiceTypeCode { get; set; }
        public string ImportType { get; set; }
        public int SlpCode { get; set; }
        public string NumberJob { get; set; }
        public string EWSeries { get; set; }
        public string DocEntry { get; set; } = null!;
        public string BookingID { get; set; } = null!;
        public string JobNo { get; set; } = null!;
        public string BookingDate { get; set; } = null!;
        public string Route { get; set; } = null!;
        public string Origin { get; set; } = null!;
        public string Destination { get; set; } = null!;
        public string SaleEmployee { get; set; } = null!;
        public string JobType { get; set; } = null!;
        public string ServiceType { get; set; } = null!;
        public List<string> Shipper { get; set; } = null!;
        public List<string> Consignee { get; set; } = null!;
        public List<string> SalesQuotation { get; set; } = null!;
        public List<string> Commodity { get; set; } = null!;
        public string GoodsDescription { get; set; } = null!;
        public string TotalPackage { get; set; } = null!;
        public double NetWeight { get; set; }
        public double GrossWeight { get; set; }
        public string LoadingDate { get; set; } = null!;
        public string CrossBorderDate { get; set; } = null!;
        public string ETARequirement { get; set; } = null!;
        public List<string> OverseaTrucker { get; set; } = null!;
        public List<PlaceOfLoading> PlaceOfLoading { get; set; } = null!;
        public List<PlaceOfDelivery> PlaceOfDelivery { get; set; } = null!;
        public List<string> ThaiForwarder { get; set; } = null!;
        public List<Volume> Volume { get; set; } = null!;
        public List<string> OverseaForwarder { get; set; } = null!;
        public List<TruckType> TruckType { get; set; } = null!;
        public List<string> ThaiBorder { get; set; } = null!;
        public string Remark { get; set; } = null!;
        public string SpecialRequest { get; set; } = null!;
        public string LoloYard { get; set; }
        public string LoloYardRemark { get; set; }
        public string FCLLCL { get; set; }
        public string CYCFS { get; set; }
        public string DG { get; set; }
        public string OtherRemark { get; set; }
    }
}
