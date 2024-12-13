using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;

namespace Client.Repository
{
    public interface IBookingSheet
    {
        #region POST
        Task<PostResponse> PostBookingSheet(PostBookingSheetRequest postBookingSheetRequest);
        #endregion
        #region Get
        Task<ResponseData<List<GetSaleEmployee>>> GetSaleEmployeeResponseAsync();
        Task<ResponseData<List<GetRoute>>> GetRouteResponseAsync();
        Task<ResponseData<List<GetImportType>>> GetImportTypeResponseAsync();
        Task<ResponseData<List<GetOrigin>>> GetOriginResponseAsync();
        Task<ResponseData<List<GetDestination>>> GetDestinationResponseAsync();
        Task<ResponseData<List<GetShipper>>> GetShipperResponseAsync();
        Task<ResponseData<List<GetCO>>> GetCOResponseAsync();
        Task<ResponseData<List<GetConsignee>>> GetConsigneeResponseAsync();
        Task<ResponseData<List<GetThaiTrucker>>> GetThaiTruckerResponseAsync();
        Task<ResponseData<List<GetOverseaTrucker>>> GetOverseaTruckerResponseAsync();
        Task<ResponseData<List<GetLOLOYARDORUNLOADING>>> GetLOLOYARDORUNLOADINGResponseAsync();
        Task<ResponseData<List<GetThaiForwarder>>> GetThaiForwarderResponseAsync();
        Task<ResponseData<List<GetOverseaForwarder>>> GetOverseaForwarderResponseAsync();
        Task<ResponseData<List<GetThaiBorder>>> GetThaiBorderResponseAsync();
        Task<ResponseData<List<GetLCLORFCL>>> GetLCLORFCLResponseAsync();
        Task<ResponseData<List<GetCYORCFS>>> GetCYORCFSResponseAsync();
        Task<ResponseData<List<GetContainerType>>> GetContainerTypeResponseAsync();
        Task<ResponseData<List<GetPlaceOfLoading>>> GetPlaceOfLoadingResponseAsync();
        Task<ResponseData<List<GetPlaceOfDelivery>>> GetPlaceOfDeliveryResponseAsync();
        Task<ResponseData<List<GetVolume>>> GetVolumeResponseAsync();
        Task<ResponseData<GetDocNumBookingSheet>> GetDocNumBookingSheetResponseAsync();
        Task<ResponseData<List<GetBookingSheetByUser>>> GetBookingSheetByUserResponseAsync(string dateFrom, string dateTo, string value, string userID);
        Task<ResponseData<List<GetServiceType>>> GetServiceTypeResponseAsync();
        Task<ResponseData<List<GetListSaleQuotation>>> GetListSaleQuotationResponseAsync();
        Task<ResponseData<List<GetTruckType>>> GetTruckTypeResponseAsync();
        Task<ResponseData<GetDetailViewBookingSheet>> GetDetailViewBookingSheetResponseAsync(string docEntry);
        Task<GetDetailBookingSheetByDocEntryResponse> GetBookingSheetByDocEntryResponseAsync(string docEntry);
        Task<ResponseData<PostBookingSheetRequest>> GetBookingSheetUpdateByDocEntryResponseAsync(string docEntry);
        Task<ResponseData<List<GetDistrict>>> GetDistrictsResponseAsync();
        #endregion
        #region Delete
        Task<DeleteResponse> CancelBookingSheetAsync(string docNum);
        #endregion
        #region Put
        Task<PostResponse> PutBookingSheet(PostBookingSheetRequest postBookingSheetRequest);
        Task<PostResponse> PutCommodityInBookingSheet(string CreateUser, int docEntry, List<Commodity> commodity, HeaderCommodityUpdate headerCommodityUpdate, List<OverTruckers> overseaTrucker
                                                                                , List<ThaiForwarders> thaiForwarders, List<OverForwarders> overForwarders
                                                                                , List<ThaiBorders> thaiBorders, PlaceOfLoading placeOfLoadings, PlaceOfLoading placeOfDeliveries);

        #endregion
    }
}
