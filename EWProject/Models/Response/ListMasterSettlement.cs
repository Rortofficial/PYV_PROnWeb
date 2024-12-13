using Client.Models.Gets;

namespace Client.Models.Response
{
    public class ListMasterSettlement
    {
        public List<GetListCustomerJobSheetTrucking> ListCustomerJobSheetTrucking { get; set; }
        public List<GetItemCodePurchaseRequest> ListItemCodePurchaseRequest { get; set; } = null!;
    }
}
