using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.CreditNoteSeaAir
{
    public class CreditNoteSeaAirs
    {
        public static Add _Add(PurchaseRequest postPurchaseRequestRequest, string CostCenter, Company _company) => new Add(postPurchaseRequestRequest, CostCenter, _company);
        public static Remove _Remove(int docEntry, Company _company) => new Remove(docEntry, _company);
    }
}
