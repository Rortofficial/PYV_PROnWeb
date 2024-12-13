using Client.Models.Gets;
using Client.Models.Request;

namespace Client.Models.Response
{
    public class GetJobSheetTruckingDetailResponse
    {
        public string ErrorMessage { get; set; } = null!;
        public int ErrorCode { get; set; }
        public List<PostJobSheetTruckingRequest> Data { get; set; } = null!;
        public List<GetLayoutShowByType> ListLayoutPrint { get; set; } = null!;
    }
}
