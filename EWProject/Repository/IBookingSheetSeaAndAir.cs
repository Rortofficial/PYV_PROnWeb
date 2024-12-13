using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;

namespace Client.Repository
{
    public interface IBookingSheetSeaAndAir
    {
        #region Get
        Task<ResponseData<List<GetOrigin>>> GetOriginResponseAsync();
        Task<ResponseData<List<GetDestination>>> GetDestinationResponseAsync();
        Task<ResponseData<List<GetJobNumberSeaAndAir>>> GetJobNumberSeaAndAirResponseAsync();
        Task<ResponseData<List<GetCoLoaderSeaAndAir>>> GetCoLoaderSeaAndAirResponseAsync();
        Task<ResponseData<List<GetCustomerSeaAndAir>>> GetCustomerSeaAndAirResponseAsync();
        Task<ResponseData<List<GetShippingLineSeaAndAir>>> GetShippingLineSeaAndAirResponseAsync();
        Task<ResponseData<List<Get_DEST_AGENTSeaAndAir>>> Get_DEST_AGENTSeaAndAirResponseAsync();
        Task<ResponseData<List<GetPortOfReceiptSeaAndAir>>> GetPortOfReceiptSeaAndAirResponseAsync();
        Task<ResponseData<List<GetPortOfDischargeSeaAndAir>>> GetPortOfDischargeSeaAndAirResponseAsync();
        Task<ResponseData<List<GetCYAtOrContactPersonSeaAndAir>>> GetCYAtOrContactPersonSeaAndAirResponseAsync();
        Task<ResponseData<List<GetReturnAtOrContactPersonSeaAndAir>>> GetReturnAtOrContactPersonSeaAndAirResponseAsync();
        Task<ResponseData<List<GetLoadingAtSeaAndAir>>> GetLoadingAtSeaAndAirResponseAsync();
        Task<ResponseData<List<GetBookingSheetByUser>>> GetBookingSheetByUserSeaAndAirResponseAsync(string dateFrom, string dateTo, string field, string userID);
        Task<ResponseData<GetBookingSheetSeaAndAir>> GetBookingSheetByDocEntrySeaAndAirResponseAsync(string docEntry);
        #endregion
        #region Post
        Task<PostResponse> PostBookingSheetSeaAndAir(AddBookingSheetSeaAndAir addBookingSheetSeaAndAir);
        #endregion
        #region Put
        Task<PostResponse> PutBookingSheetSeaAndAir(AddBookingSheetSeaAndAir addBookingSheetSeaAndAir);
        Task<PostResponse> PutCommodityInBookingSheet(string CreateUser, int docEntry, List<CommoditySeaAndAir> commodity, HeaderCommodityUpdate headerCommodityUpdate, List<DestAgentUpdateCommodities> listDestAgent, List<ShipperUpdateCommodities> listShipper, List<ConsigneeUpdateCommodities> listConsignee, List<CustomerUpdateCommodities> listCustomer);
        #endregion
        #region Delete
        Task<DeleteResponse> CancelBookingSheetSeaAndAirAsync(string docNum);
        #endregion
    }
}
