using Client.Models.Gets;
using Client.Models.Request;

namespace Client.Models.Response
{
    public class ListMasterJobSheetTruckingSeaAir
    {
        public ResponseData<GetJobSheetTruckingEdit> GetJobSheetTruckingEditResponses { get; set; }
        public List<GetDetailJobSheetTrucking> GetDetailJobSheetTruckings { get; set; } = null!;//PostJobSheetTruckingSeaAirRequest
        public List<GetItemDetailByItemType> GetItemDetailByItemTypes { get; set; } = null!;
        public List<GetSaleQuotationJobSheet> GetSaleQuotationJobSheets { get; set; } = null!;
        public PostJobSheetTruckingSeaAirRequest GetJobSheetTruckings { get; set; }
        public List<GetVendorByConfirmJobSheetSeaAir> GetVendorByConfirmJobSheetSeaAirs { get; set; }
        public List<GetLayoutShowByType> ListLayoutPrint { get; set; } = null!;
    }
}
