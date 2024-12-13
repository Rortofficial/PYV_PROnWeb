using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.CreditNote
{
    public class CreditNotes
    {
        public static Add _Add(PostARCreditMemoRequest postARCreditMemoRequest, Company _company) => new Add(postARCreditMemoRequest, _company);
    }
}
