using Client.Models.Gets;
using Client.Models.Request;
using Client.Models.Response;

namespace Client.Repository
{
    public interface IDeditNote
    {
        Task<ResponseData<List<GetARInvoice>>> GetARInvoiceResponseAsync(string department);
        Task<PostResponse> postARDeditMemoResponse(PostARCreditMemoRequest postARCreditMemoRequest);
        Task<ResponseData<List<GetAdvancePaymentClearing>>> GetCreditNoteListResponseAsync(string dateFrom, string dateTo, string type, string userID);
        Task<ResponseData<GetARInvoiceWithJobNumberByDocEntry>> GetARInvoiceWithJobNumberByDocEntryResponseAsync(string docEntry, string department);

    }
}
