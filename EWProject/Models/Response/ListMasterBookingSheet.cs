using Client.Models.Gets;

namespace Client.Models.Response
{
    public class ListMasterBookingSheet
    {
        //public string SalesQuotation { get; set; } = null!;
        public List<GetSaleEmployee> SaleEmployees { get; set; }
        public List<GetRoute> GetRoutes { get; set; }
        public List<GetImportType> GetImportTypes { get; set; }
        public List<GetOrigin> GetOrigins { get; set; }
        public List<GetDestination> GetDestinations { get; set; }
        public List<GetShipper> GetShippers { get; set; }
        public List<GetCO> GetCOs { get; set; }
        public List<GetConsignee> GetConsignees { get; set; }
        public List<GetThaiTrucker> GetThaiTruckers { get; set; }
        public List<GetOverseaTrucker> GetOverseaTruckers { get; set; }
        public List<GetLOLOYARDORUNLOADING> GetLOLOYARDORUNLOADINGs { get; set; }
        public List<GetThaiForwarder> GetThaiForwarders { get; set; }
        public List<GetOverseaForwarder> GetOverseaForwarders { get; set; }
        public List<GetThaiBorder> GetThaiBorders { get; set; }
        public List<GetLCLORFCL> GetLCLORFCLs { get; set; }
        public List<GetCYORCFS> GetCYORCFSs { get; set; }
        public List<GetContainerType> GetContainerTypes { get; set; }
        public List<GetPlaceOfLoading> GetPlaceOfLoadings { get; set; }
        public List<GetPlaceOfDelivery> GetPlaceOfDeliveries { get; set; }
        public List<GetVolume> GetVolumes { get; set; }
        public GetDocNumBookingSheet GetDocNumBookingSheets { get; set; }
        public List<GetServiceType> GetServiceTypes { get; set; }
        public List<GetTruckType> GetTruckTypes { get; set; }
        public List<GetListSaleQuotation> GetListSaleQuotations { get; set; }
        public List<GetDistrict> GetDistricts { get; set; }
        public GetDetailBookingSheetByDocEntryResponse GetDetailBookingSheetByID { get; set; }
    }
}
