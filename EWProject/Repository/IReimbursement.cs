using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;

namespace Client.Repository
{
    public interface IReimbursement
    {
        Task<ResponseData<List<ReimbursementRequest>>> ReimbursementGet(string docEntry, string dateFrom, string dateTo, string type, string userID);
        Task<ResponseData<List<ReimbursementRequest>>> ListReimbursementApprove();
        Task<ResponseData<List<GetAccountByEmployeeBudget>>> GetEmployeeBudget();
        Task<ResponseData<List<GetAccountReimbursment>>> GetAccountCode();
        Task<PostResponse> ReimbursementPost(ReimbursementRequest reimbursementRequest);
        Task<PostResponse> ReimbursementPut(ReimbursementRequest reimbursementRequest);
        Task<PostResponse> ReimbursementDelete(string docEntry);
        Task<PostResponse> ApproveReimbursementPost(List<ListApproveReimbursement> listApproveReimbursements, string CostCenter);
    }
}