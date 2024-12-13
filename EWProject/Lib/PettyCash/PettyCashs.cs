using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.PettyCash
{
    public class PettyCashs
    {
        public static Add _Add(PostPettyCashRequest postPettyCashRequest, string draftJE,string CostCenter, Company _company) => new Add(postPettyCashRequest, draftJE, CostCenter, _company);
        public static AddDraft _AddDraft(PostPettyCashRequest postPettyCashRequest, Company _company) => new AddDraft(postPettyCashRequest, _company);
        public static CancelDraft _CancelDraft(string docNum, Company _company) => new CancelDraft(docNum, _company);
        public static CloseDraft _CloseDraft(string docNum, Company _company) => new CloseDraft(docNum, _company);
        public static UpdateDraft _UpdateDraft(string docEntry, string docEntryJE, PostPettyCashRequest postPettyCashRequest, Company _company) => new UpdateDraft(docEntry, docEntryJE, postPettyCashRequest, _company);

    }
}
