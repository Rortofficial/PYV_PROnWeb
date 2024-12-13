using Client.Models.Gets;

namespace Client.Models.Response
{
    public class ListMasterEditSettlement
    {
        public List<GetItemCodePurchaseRequest> ListItemCodePurchaseRequest { get; set; } = null!;
        public GetDetailInformationDocumentApproveResponse GetDetailSettlementDraft { get; set; }
    }
}
