using Client.Models.Gets;

namespace Client.Models.Response
{
    public class ListMasterBookingSheetSeaAndAir
    {
        public ResponseData<List<GetJobNumberSeaAndAir>> GetJobNumberSeaAndAirs { get; set; }
        public ResponseData<List<GetSaleEmployee>> SaleEmployees { get; set; }
        public ResponseData<List<GetPlaceOfLoading>> GetPlaceOfLoadings { get; set; }
        public ResponseData<List<GetPlaceOfDelivery>> GetPlaceOfDeliveries { get; set; }
        public ResponseData<List<GetOverseaForwarder>> GetOverseaForwarders { get; set; }
        public ResponseData<List<GetOverseaTrucker>> GetOverseaTruckers { get; set; }
        public ResponseData<List<GetOrigin>> GetOrigins { get; set; }
        public ResponseData<List<GetDestination>> GetDestinations { get; set; }
        public ResponseData<List<GetShipper>> GetShippers { get; set; }
        public ResponseData<List<GetCoLoaderSeaAndAir>> GetCoLoaderSeaAndAirs { get; set; }
        public ResponseData<List<GetCustomerSeaAndAir>> GetCustomerSeaAndAirs { get; set; }
        public ResponseData<List<GetConsignee>> GetConsignees { get; set; }
        public ResponseData<List<GetShippingLineSeaAndAir>> GetShippingLineSeaAndAirs { get; set; }
        public ResponseData<List<Get_DEST_AGENTSeaAndAir>> Get_DEST_AGENTSeaAndAirs { get; set; }
        public ResponseData<List<GetPortOfReceiptSeaAndAir>> GetPortOfReceiptSeaAndAirs { get; set; }
        public ResponseData<List<GetPortOfDischargeSeaAndAir>> GetPortOfDischargeSeaAndAirs { get; set; }
        public ResponseData<List<GetThaiForwarder>> GetThaiForwarders { get; set; }
        public ResponseData<List<GetContainerType>> GetContainerTypes { get; set; }
        public ResponseData<List<GetThaiBorder>> GetThaiBorders { get; set; }
        public ResponseData<List<GetCYAtOrContactPersonSeaAndAir>> GetCYAtOrContactPersonSeaAndAirs { get; set; }
        public ResponseData<List<GetReturnAtOrContactPersonSeaAndAir>> GetReturnAtOrContactPersonSeaAndAirs { get; set; }
        public ResponseData<List<GetLoadingAtSeaAndAir>> GetLoadingAtSeaAndAirs { get; set; }
        public ResponseData<List<GetTruckType>> GetTruckTypes { get; set; }
        public ResponseData<GetBookingSheetSeaAndAir> getDetailBookingSheetSeaAndAir { get; set; }
    }
}
