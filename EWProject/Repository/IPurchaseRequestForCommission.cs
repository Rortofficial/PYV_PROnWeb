using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;

namespace Client.Repository
{
    public interface IPurchaseRequestForCommission
    {
        Task<ResponseData<List<GetListSalesCommission>>> GetListSalesCommissions(string dateFrom, string dateTo, string type, string userID);
        Task<ResponseData<List<GetCommissionBySaleEmployee>>> getCommissionBySaleEmployee(string slpCode);
        Task<ResponseData<PostSalesCommissions>> getSalesCommissionDetailByDocEntry(string docEntry);
        Task<ResponseData<PostSalesCommissionsRow>> CheckCommissionDetailByJobNumber(PostInvoiceListSalesCommission jobNumber);
        Task<PostResponse> PostCommissionBySaleEmployee(PostSalesCommissions postSalesCommissions);
        Task<DeleteResponse> CancelCommissionBySaleEmployee(string docNum);
        Task<DeleteResponse> RejectCommissionBySaleEmployee(string docNum, string remark);
        Task<PostResponse> ApproveCommissionBySaleEmployee(PostSalesCommissions postSalesCommissions, string CostCenter);
    }
}
