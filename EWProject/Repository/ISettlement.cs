using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;

namespace Client.Repository
{
    public interface ISettlement
    {
        Task<PostResponse> PutPurchaseRequestAsync(UpdatePurchaseRequest updatePurchaseRequest,string CostCenter);
        Task<PostResponse> AddSettlementDraftAsync(AddSettlementDraftRequest addSettlementDraftRequest);
        Task<PostResponse> UpdateSettlementDraftAsync(AddSettlementDraftRequest addSettlementDraftRequest);
        Task<PostResponse> CancelSettlementDraftAsync(string docNum);
        Task<ResponseData<List<GetListCustomerJobSheetTrucking>>> getListCustomerJobSheetTruckingResponse(string JobNumber);
        Task<ResponseData<List<GetListRefInvByJobSheet>>> getListRefInvByJobSheetResponse(string CardCode, string JobNumber);
        Task<ResponseData<List<GetProjectManagmentList>>> GetProjectManagmentListResponseAsync();
        Task<GetDetailInformationDocumentApproveResponse> GetDetailPurchaseOrderResponseAsync(string docNum);
        Task<ResponseData<List<GetAdvancePaymentClearing>>> GetPurchaseOrderListResponseAsync(string dateFrom, string dateTo, string type, string userID);
        Task<GetDetailInformationDocumentApproveResponse> GetDetailSettlementByDocEntryResponseAsync(string docNum);
    }
}