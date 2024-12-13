using Client.Lib.DebitNoteSeaAir;
using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.DebitSeaAir
{
    public class DebitSeaAirs
    {
        public static Add _Add(PurchaseRequest postPurchaseRequestRequest, string CostCenter, Company _company) => new Add(postPurchaseRequestRequest, CostCenter, _company);
    }
}
