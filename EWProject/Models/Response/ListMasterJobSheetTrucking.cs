using Client.Models.Gets;
using Client.Models.Request;

namespace Client.Models.Response
{
    public class ListMasterJobSheetTrucking
    {
        public ResponseData<GetJobSheetTruckingEdit> GetJobSheetTruckingEditResponses { get; set; }
        public List<GetDetailJobSheetTrucking> GetDetailJobSheetTruckings { get; set; }
        public List<GetItemDetailByItemType> GetItemDetailByItemTypes { get; set; }
        public List<GetSaleQuotationJobSheet> GetSaleQuotationJobSheets { get; set; }
        public List<PostJobSheetTruckingRequest> GetJobSheetTruckings { get; set; }
        public List<GetJobSheetTruckingEditDraftByDocEntry> GetJobSheetTruckingEditDrafts { get; set; }
        public List<GetUomGroup> GetUomGroups { get; set; }
        public List<GetVendorByConfirmJobSheetSeaAir> GetVendorByConfirmJobSheetSeaAirs { get; set; }
        public List<GetLayoutShowByType> ListLayoutPrint { get; set; }
    }
}
