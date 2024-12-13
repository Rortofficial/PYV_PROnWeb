using Client.Models.Gets;
using Client.Models.Request;

namespace Client.Models.Response
{
    public class GetDetailAdvancePaymentByDocEntryResponse
    {
        public string ErrorMessage { get; set; } = null!;
        public int ErrorCode { get; set; }
        public PostAdvancePaymentRequest Data { get; set; } = null!;
        public List<GetLayoutShowByType> ListLayoutPrint { get; set; } = null!;
    }
}
