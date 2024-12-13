using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;

namespace Client.Repository
{
    public interface IPettyCash
    {
        #region Get
        Task<ResponseData<string>> GetMaxJournalVoucherNumberResponseAsync();
        Task<ResponseData<List<GetSeriesJournalVoucher>>> GetSeriesJournalVoucherResponseAsync(string type);
        Task<ResponseData<List<GetBPCodeJournalVoucher>>> GetBPCodeJournalVoucherResponseAsync();
        Task<ResponseData<List<GetAccountCodeJournalVoucher>>> GetAccountCodeJournalVoucherResponseAsync();
        Task<ResponseData<List<GetVatGroupJournalVoucher>>> GetVatGroupJournalVoucherResponseAsync();
        Task<ResponseData<List<GetListJournalVoucher>>> GetListJournalVoucherResponseAsync(string dateFrom, string dateTo, string type, string userID);
        Task<ResponseData<PettyCashDetail>> GetViewDetailPettyCashResponseByDocEntryAsync(string docEntry, string type);
        Task<ResponseData<GetAccountByEmployeeBudget>> GetEmployeeBudgetByIDAsync(string empID);

        #endregion
        #region POST
        Task<PostResponse> PostPettyCash(PostPettyCashRequest postPettyCashRequest, string draftJE, string CostCenter);
        Task<PostResponse> PostPettyCashDraft(PostPettyCashRequest postPettyCashRequest);
        #endregion
        #region Delete
        Task<PostResponse> CancelPettyCashDraft(string docEntry);
        #endregion
        #region Close
        Task<PostResponse> ClosePettyCashDraft(string docEntry);
        #endregion
        #region PUT
        Task<PostResponse> PutPettyCash(string docEntry, string JEDocEntry, PostPettyCashRequest postPettyCashRequest);
        #endregion
    }
}
