using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;

namespace Client.Repository
{
    public interface IJobSheetTruckingSeaAndAir
    {
        Task<PostResponse> PostJobSheetTrucking(PostJobSheetTruckingSeaAirRequest postJobSheetTruckingRequest, string draftdocument,string CostCenter);
        Task<PostResponse> PutDraftJobSheetTrucking(PostJobSheetTruckingSeaAirRequest postJobSheetTruckingRequest, string draftdocument,string jobSheetDocEntry, string CostCenter);
        Task<PostResponse> PutJobSheetTrucking(PostJobSheetTruckingSeaAirRequest updateJobSheetTruckingEdit, string jobDocEntry,string CostCenter);
        Task<PostResponse> DeleteJobSheetTrucking(string jobDocEntry,int soDocEntry,string UpdateBy);
        Task<ResponseData<GetJobSheetTruckingEdit>> GetJobSheetTruckingEditResponsesAsync(int confirmBookingSheetID, int jobSheetDocEntry);
        Task<ResponseData<List<GetDetailJobSheetTrucking>>> GetConfirmBookingSheetDetailByUserResponseAsync(int confirmBookingSheetID);
        Task<ResponseData<List<GetVendorByConfirmJobSheetSeaAir>>> GetVendorByConfirmJobSheetSeaAirResponseAsync(int confirmBookingSheetID);
        Task<ResponseData<List<GetItemDetailByItemType>>> GetItemDetailByItemTypeResponseAsync(string userID, string department);
        Task<ResponseData<List<GetConfirmBookingSheetByUser>>> GetConfirmBookingSheetByUserResponse();
        Task<ResponseData<List<GetSaleQuotationJobSheet>>> GetSaleQuotationJobSheetResponse(int ConfirmBooking, int UserID);
        //Task<GetItemSaleQuotationJobSheetResponse> GetGetItemSaleQuotationJobSheetResponse(int DocEntry);
        Task<ResponseData<List<GetListJobSheetTrucking>>> GetListJobSheetTruckingResponses(string dateFrom, string dateTo, string type, string userID);
        Task<ResponseData<PostJobSheetTruckingSeaAirRequest>> GetJobSheetTruckingDetailByDocEntry(int docEntry);
        Task<ResponseData<List<GetCurrencyByCustomer>>> GetCurrecnyByCardCode(string CardCode);
        //Task<ResponseData<List<GetUomGroup>>> GetUomGroups(string uomEntry);
    }
}
