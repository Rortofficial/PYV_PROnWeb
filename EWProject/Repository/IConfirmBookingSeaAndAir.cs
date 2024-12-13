using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;

namespace Client.Repository
{
    public interface IConfirmBookingSeaAndAir
    {
        Task<ResponseData<List<GetBookingSheetByUser>>> GetBookingSheetByUserResponseAsync();
        Task<ResponseData<List<GetDetailBookingSheetByUser>>> GetDetailBookingSheetByUserResponseAsync(int BookingID);
        Task<ResponseData<List<GetListConfirmBookingSheet>>> GetListConfirmBookingSheetResponseAsync(string dateFrom, string dateTo, string type, string userID);
        Task<GetDetailConfirmBookingSheetByDocEntryResponse> GetDetailConfirmBookingSheetByDocEntryResponseAsync(int DocEntry);
        Task<ResponseData<List<GetEditConfirmBookingSheet>>> GetEditConfirmBookingSheetByDocEntryResponseAsync(int DocEntry);
        Task<ResponseData<List<GetDetailBookingSheetByUser>>> GetEditDetailBookingSheetByUserResponseAsync(int BookingID);
        #region Post
        Task<PostResponse> PostConfirmBookingSheet(PostConfirmBookingSheetRequest postConfirmBookingSheetRequest);
        #endregion
        #region Put
        Task<PostResponse> PutConfirmBookingSheet(PutConfirmBookingSheetRequest putConfirmBookingSheetRequest);
        #endregion
        #region Cancel
        Task<DeleteResponse> CancelConfirmBookingSheetAsync(string docNum, string bookingDocEntry, string projectManagementEntry, string projectSeries);
        #endregion
    }
}
