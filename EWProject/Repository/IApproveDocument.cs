using Client.Models.Gets;
using Client.Models.Response;

namespace Client.Repository
{
    public interface IApproveDocument
    {
        Task<ResponseData<List<GetListDocumentApproveByType>>> GetListDocumentApproveByTypeResponseAsync(string dateFrom, string dateTo, string documentType);
        Task<GetDetailInformationDocumentApproveResponse> GetDetailInformationDocumentApproveResponseAsync(string docNum);
        Task<DeleteResponse> OnRejectPurchaseRequestResponseAsync(string docNum, string RemarksReson, string RejectBy);
        Task<DeleteResponse> OnApprovePurchaseRequestResponseAsync(string docNum, string RemarksReson,string ApproveBy,string CostCenter);
    }
}
