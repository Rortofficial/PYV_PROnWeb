using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;

namespace Client.Repository
{
    public interface ICreditNote
    {
        Task<ResponseData<List<GetARInvoice>>> GetARInvoiceResponseAsync(string Department);
        Task<ResponseData<GetARInvoiceWithJobNumberByDocEntry>> GetARInvoiceWithJobNumberByDocEntryResponseAsync(string docEntry);
        Task<PostResponse> postARCreditMemoResponse(PostARCreditMemoRequest postARCreditMemoRequest);
        Task<ResponseData<List<GetAdvancePaymentClearing>>> GetCreditNoteListResponseAsync(string dateFrom, string dateTo, string value, string userID);
        Task<GetDetailInformationDocumentApproveResponse> GetDetailCreditMemoResponseAsync(string docNum,string LayoutType);
    }
}
