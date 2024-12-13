using Client.Models.Request;
using SAPbobsCOM;

namespace Client.Lib.Settlement
{
    public class Settlements
    {
        public static Add _Add(AddSettlementDraftRequest addSettlementDraftRequest, Company _company) => new Add(addSettlementDraftRequest, _company);
        public static Update _Update(AddSettlementDraftRequest addSettlementDraftRequest, Company _company) => new Update(addSettlementDraftRequest, _company);
        public static Cancel _Cancel(string docNum, Company _company) => new Cancel(docNum, _company);
        public static ClearEmptyStringLine _ClearEmptyStringLine(List<AddSettlementLine> Lines) => new ClearEmptyStringLine(Lines);
    }
}
