using Client.Models.Gets;
using Client.Models.Request;

namespace Client.Models.Response
{
    public class GetDetailInformationDocumentApproveResponse
    {
        public string ErrorMessage { get; set; } = null!;
        public int ErrorCode { get; set; }
        public PurchaseRequest Data { get; set; } = null!;
        public List<GetLayoutShowByType> ListLayoutPrint { get; set; } = null!;
    }
}
