using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;

namespace Client.Repository
{
    public interface IConfirmBookingSheet
    {
        #region POST
        Task<PostResponse> PostConfirmBookingSheet(PostConfirmBookingSheetRequest postConfirmBookingSheetRequest);
        #endregion
        #region PUT
        Task<PostResponse> PutConfirmBookingSheet(PutConfirmBookingSheetRequest postConfirmBookingSheetRequest);
        #endregion
        #region GET
        Task<ResponseData<List<GetListConfirmBookingSheet>>> GetListConfirmBookingSheetResponseAsync(string dateFrom, string dateTo, string type, string userID);
        Task<ResponseData<List<GetBookingSheetByUser>>> GetBookingSheetByUserResponseAsync();
        Task<ResponseData<List<GetDetailBookingSheetByUser>>> GetDetailBookingSheetByUserResponseAsync(int BookingID);
        Task<ResponseData<List<GetDetailBookingSheetByUser>>> GetEditDetailBookingSheetByUserResponseAsync(int BookingID);
        Task<ResponseData<List<GetContainer>>> GetContainerResponseAsync(string docEntry);
        Task<ResponseData<List<GetVendor>>> GetVendorResponseAsync();
        Task<ResponseData<List<GetTruckNo>>> GetTruckNoResponseAsync(string Province);
        Task<ResponseData<List<GetTrailerNo>>> GetTrailerNoResponseAsync();
        Task<ResponseData<List<GetTruckProvince>>> GetTruckProvinceResponseAsync();
        Task<ResponseData<List<GetEmployee>>> GetEmployeeResponseAsync();
        Task<ResponseData<List<GetCostType>>> GetCostTypeResponseAsync();
        Task<ResponseData<List<GetPriceListConfirmBooking>>> GetPriceListConfirmBookingResponseAsync();
        Task<ResponseData<List<CSName>>> GetCSNameResponseAsync();
        Task<GetDetailConfirmBookingSheetByDocEntryResponse> GetDetailConfirmBookingSheetByDocEntryResponseAsync(int DocEntry);
        Task<ResponseData<List<GetEditConfirmBookingSheet>>> GetEditConfirmBookingSheetByDocEntryResponseAsync(int DocEntry);
        #endregion
        #region Delete
        Task<DeleteResponse> CancelConfirmBookingSheetAsync(string docNum, string bookingDocEntry, string projectManagementEntry, string projectSeries);
        #endregion
    }
}
