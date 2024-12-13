using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.JournalEntry
{
    public class JournalEntrys
    {
        public static Add _Add(PostAdvancePaymentRequest postAdvancePaymentRequest, Company _company) => new Add(postAdvancePaymentRequest, _company);
    }
}
