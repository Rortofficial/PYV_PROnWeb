using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.ARDebitNote
{
    public class ARDebitNotes
    {
        public static Add _Add(PostARCreditMemoRequest postARCreditMemoRequest, Company _company) => new Add(postARCreditMemoRequest, _company);
    }
}
