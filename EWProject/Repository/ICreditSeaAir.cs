using Client.Models.Gets;
using Client.Models.Response;

namespace Client.Repository
{
    public interface ICreditSeaAir
    {
        Task<ResponseData<List<GetAdvancePaymentClearing>>> GetListOfCreditNote(string dateFrom, string dateTo, string type, string userID);
        Task<ResponseData<List<GetAdvancePaymentClearing>>> GetListOfDebitNote(string dateFrom, string dateTo, string type, string userID);
        Task<ResponseData<List<GetItemCodePurchaseRequest>>> GetItemCodeCreditSeaAirResponsesAsync(string type, string department);
        Task<ResponseData<List<GetProjectManagmentList>>> GetProjectManagmentListResponseAsync();
        Task<ResponseData<List<GetTaxCode>>> GetTaxCodes(string type);
        Task<ResponseData<List<GetArCreditNoteRefInvSeaAndAir>>> GetArCreditNoteRefInvSeaAndAir();
        Task<PostResponse> PostCreditSeaAir(Models.Request.PurchaseRequest postPurchaseRequestRequest,string CostCenter);
        Task<PostResponse> PostDebitSeaAir(Models.Request.PurchaseRequest postPurchaseRequestRequest, string CostCenter);
        Task<PostResponse> RemoveCreditSeaAir(int docEntry);
        Task<ResponseData<Models.Request.PurchaseRequest>> GetCreditSeaAirDeatailByDocEntry(string docEntry, string type);
    }
}
