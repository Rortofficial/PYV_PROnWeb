using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;

namespace Client.Repository
{
    public interface IJobSheetTrucking
    {
        Task<PostResponse> PostJobSheetTrucking(PostJobSheetTruckingRequest postJobSheetTruckingRequest, string draftDocument);
        Task<PostResponse> PutJobSheetTruckingDraft(UpdateDraftJobSheetTrucking updateJobSheetTruckingEdit, string draftDocument);
        Task<PostResponse> PutJobSheetTrucking(GetJobSheetTruckingEdit updateJobSheetTruckingEdit);
        Task<PostResponse> PutAttachmentJobSheetTrucking(GetJobSheetTruckingEdit updateJobSheetTruckingEdit);
        Task<PostResponse> CancelJobSheetTrucking(string docEntry, string docEntrySO, string updateBy);
        Task<ResponseData<GetJobSheetTruckingEdit>> GetJobSheetTruckingEditResponsesAsync(int confirmBookingSheetID, int jobSheetDocEntry);
        Task<ResponseData<List<GetDetailJobSheetTrucking>>> GetConfirmBookingSheetDetailByUserResponseAsync(int confirmBookingSheetID);
        Task<ResponseData<List<GetItemDetailByItemType>>> GetItemDetailByItemTypeResponseAsync(string userID, string department);
        Task<ResponseData<List<GetConfirmBookingSheetByUser>>> GetConfirmBookingSheetByUserResponse();
        Task<ResponseData<List<GetSaleQuotationJobSheet>>> GetSaleQuotationJobSheetResponse(int ConfirmBooking, int UserID);
        //Task<GetItemSaleQuotationJobSheetResponse> GetGetItemSaleQuotationJobSheetResponse(int DocEntry);
        Task<ResponseData<List<GetListJobSheetTrucking>>> GetListJobSheetTruckingResponses(string dateFrom, string dateTo, string type, string userID);
        Task<GetJobSheetTruckingDetailResponse> GetJobSheetTruckingDetailByDocEntry(int docEntry);
        Task<ResponseData<List<GetJobSheetTruckingEditDraftByDocEntry>>> GetJobSheetTruckingEditDraftByDocEntry(int docEntry);
        Task<ResponseData<List<GetCurrencyByCustomer>>> GetCurrecnyByCardCode(string CardCode, string confirmBookingID);
        Task<ResponseData<List<GetUomGroup>>> GetUomGroups(string uomEntry);
    }
}
