using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;

namespace Client.Repository
{
    public interface IAdvancePayment
    {
        #region Get
        Task<ResponseData<List<GetProjectManagmentList>>> GetProjectManagmentListResponseAsync();
        Task<ResponseData<List<GetAdvancePayment>>> GetAdvancePaymentByUserResponseAsync();
        Task<ResponseData<List<GetAdvancePayment>>> GetAdvancePaymentApprovalResponseAsync();
        Task<GetDetailAdvancePaymentByDocEntryResponse> GetDetailAdvancePaymentByDocEntryResponse(string docEntry);
        Task<ResponseData<List<GetAdvancePaymentClearing>>> GetAdvancePaymentClearingResponseAsync();
        Task<ResponseData<List<GetAdvancePaymentClearing>>> GetAdvancePaymentClearingResponseByTransIdAsync(string transID);
        Task<ResponseData<List<GetSeries>>> GetSeriesResponseAsync();
        Task<ResponseData<List<GetClearingAdvance>>> GetClearingAdvanceTypeReponseAsync();
        #endregion
        #region Post
        Task<PostResponse> PostAdvancePayment(PostAdvancePaymentRequest postAdvancePaymentRequest);
        Task<PostResponse> PostJE(PostAdvancePaymentRequest postAdvancePaymentRequest);
        Task<PostResponse> PostEmployeeClearingAdvance(EmployeeClearingAdvanceRequest postAdvancePaymentRequest);

        #endregion
        #region Delete
        Task<DeleteResponse> CancelAdvancePaymentAsync(string docNum);
        Task<DeleteResponse> RejectAdvancePayment(string DocEntry);
        #endregion
    }
}
